Установка плагинов Chronos из сборки Tessa.Extensions.Chronos:

1. Остановите сервис Chronos. Удостоверьтесь, что версия Chronos и используемых NuGet-пакетов Chronos.* и Tessa.* соответствует используемой версии платформы с точностью до патча.
2. Убедитесь, что в xml-файлах плагина, расположенных в папке Extensions\Tessa.Extensions.Chronos\configuration, требуемые плагины включены, т.е. у них указано disabled="false". В плагине-примере ExamplePlugin по умолчанию указано disable="true", замените его на "false", чтобы включить.
3. Соберите проект Tessa.Extensions.Chronos в IDE или командой dotnet build.
4. Скопируйте содержимое папки Bin\Tessa.Extensions.Chronos (относительно папки с файлом .sln) в папку Plugins\Tessa.Extensions.Chronos. Это ваша папка с плагинами, которая является отдельной от типовых плагинов.
5. Если в файле проекта вы подключали дополнительные NuGet-пакеты, то скопируйте их и их транзитивные зависимости в папку с плагином, но только если эти же библиотеки отсутствуют в папке с хостом Chronos (рядом с запускаемым файлом).
6. Запустите сервис Chronos в окне консоли. Названия ваших плагинов в сборке Tessa.Extensions.Chronos должны быть выведены на экране.

По умолчанию в проекте объявлен плагин ExamplePlugin, который также выполняет запись в log.txt на уровне Trace. Вы можете включить этот уровень логирования в NLog.config в папке с Chronos,
заменив строку: <logger name="*" minlevel="Trace" writeTo="file" />

Копирование можно автоматизировать при сборке, записав скрипт копирования в Extensions\Tessa.Extensions.Chronos\post-build.bat (Windows) или post-build.sh (Linux).
