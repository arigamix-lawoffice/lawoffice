# Type script templates generation script

Чтобы воспользоваться скриптом генерации ts шаблонов, создающим соответствующие конфигурации классы и поля:

1. Скопируйте файл `GenerateTemplatesWeb.sample.bat` и переименуйте его в `GenerateTemplatesWeb.bat`.
1. Отредактируйте файл скрипта, указав путь до проекта в переменной `ProjectPath`
1. Отредактируйте файл скрипта, указав путь до папки с утилитой tadmin в переменной `tadminPath`. Папка должна содержать подпапку `extensions` с актуальными бинами проекта `Tessa.Extensions.Console`.
1. Запустите скрипт `GenerateTemplatesWeb.bat`.