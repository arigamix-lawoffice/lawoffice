--[[
Замечание по использованию комментариев.
Из-за ошибки в StackExchange.Redis v2.5.61 параметрами считаются все строки начинающиеся с символа <commercial at>
без учёта является ли строка комментарием или нет.
Это не позволяет использовать комментарии вида:
---comment
---<commercial at>param <param_name> <type>
---<commercial at>return <type>

Функции, обеспечивающие блокировку правил доступа на чтение и запись.

Работа основана на значении счётчика взятых блокировок.

Возможные значения:
- n < 0 - взято n блокировок на запись;
- n > 0 - взято n блокировок на чтение;
- все остальные состояния запрещены.

Коды возврата (ObjectLockingStrategy.ResultCodes):
  0 - Команда успешно выполнена.
 -1 - Некорректная команда.
 -2 - Ключ имеет значение nil или является пустой строкой.
 -3 - Идентификатор объекта имеет значение nil или является пустой строкой.
 -5 - Блокировка отсутствует.
 -6 - Недопустимое значение счётчика.
 -7 - Существует блокировка на запись.
 -8 - Существует блокировка на чтение.
]]

---Возвращает значение, показывающее, что заданная строка равна значению nil или пустой строке.
---param s string: Проверяемая строка.
---return boolean: Значение true, если указанная строка равна значению nil или пустой строке.
local function isempty(s)
  return s == nil or string.len(s) == 0
end

---Выполняет взятие блокировки на чтение.
---param key string: Основной ключ для записей о блокировках в Redis.
---param id string: Идентификатор объекта.
---return integer: Код возврата. Возможные значения: 0, -6, -7.
local function obtainReadLock(key, id)
  local ls = redis.call('HGET', key, id)

  if not ls then
    redis.call('HSET', key, id, 1)

    -- Команда успешно выполнена.
    return 0
  end

  local value = tonumber(ls)

  if value > 0 then
    redis.call('HSET', key, id, value + 1)

    -- Команда успешно выполнена.
    return 0
  elseif value < 0 then
    -- Существует блокировка на запись.
    return -7
  else
    -- Недопустимое значение счётчика.
    return -6
  end
end

---Освобождает блокировку на чтение.
---param key string: Основной ключ для записей о блокировках в Redis.
---param id string: Идентификатор объекта.
---return integer: Код возврата. Возможные значения: 0, -5, -6, -7.
local function releaseReadLock(key, id)
  local ls = redis.call('HGET', key, id)
  if not ls then
    -- Блокировка отсутствует.
    return -5
  end

  local value = tonumber(ls)

  if value > 0 then
    value = value - 1

    if value == 0 then
      redis.call('HDEL', key, id)
    else
      redis.call('HSET', key, id, value)
    end

    -- Команда успешно выполнена.
    return 0
  elseif value < 0 then
    -- Существует блокировка на запись.
    return -7
  else
    -- Недопустимое значение счётчика.
    return -6
  end
end

---Выполняет взятие блокировки на запись.
---param key string: Основной ключ для записей о блокировках в Redis.
---param id string: Идентификатор объекта.
---return integer: Код возврата. Возможные значения: 0, -6, -8.
local function obtainWriteLock(key, id)
  local ls = redis.call('HGET', key, id)
  if not ls then
    redis.call('HSET', key, id, -1)

    -- Команда успешно выполнена.
    return 0
  end

  local value = tonumber(ls)

  if value < 0 then
    redis.call('HSET', key, id, value - 1)

    -- Команда успешно выполнена.
    return 0
  elseif value > 0 then
    -- Существует блокировка на чтение.
    return -8
  else
    -- Недопустимое значение счётчика.
    return -6
  end
end

---Освобождает блокировку на запись.
---param key string: Основной ключ для записей о блокировках в Redis.
---param id string: Идентификатор объекта.
---return integer: Код возврата. Возможные значения: 0, -5, -6, -8.
local function releaseWriteLock(key, id)
  local ls = redis.call('HGET', key, id)
  if not ls then
    -- Блокировка отсутствует.
    return -5
  end

  local value = tonumber(ls)

  if value < 0 then
    value = value + 1

    if value == 0 then
      redis.call('HDEL', key, id)
    else
      redis.call('HSET', key, id, value)
    end

    -- Команда успешно выполнена.
    return 0
  elseif value > 0 then
    -- Существует блокировка на чтение.
    return -8
  else
    -- Недопустимое значение счётчика.
    return -6
  end
end

-- Основной скрипт.
-- Выполняется с помощью StackExchange.Redis.

-- Идентификатор команды (ObjectLockingStrategy.Commands).
local command = @command

-- Основной ключ для записей о блокировках в Redis.
local key = @key

-- Идентификатор объекта.
local id = @id

if isempty(key) then
  -- Ключ имеет значение nil или является пустой строкой.
  return -2
end

if isempty(id) then
  -- Идентификатор объекта имеет значение nil или является пустой строкой.
  return -3
end

if command == '0' then
  return obtainReadLock(key, id)
elseif command == '1' then
  return releaseReadLock(key, id)
elseif command == '2' then
  return obtainWriteLock(key, id)
elseif command == '3' then
  return releaseWriteLock(key, id)
else
  -- Некорректная команда.
  return -1
end
