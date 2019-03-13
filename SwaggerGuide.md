
# Introduction 
Допустим, вам заказали создать некое приложение.  
Заказчик потребовал несколько способов взаимодействия с приложением : мобильное приложение, веб-интерфейс или что-то другое.
Вы back-end разработчик и создали WebAPI для интегрирования front-end разработки в ваше приложение. 
Однако, чтобы не обьяснять каждой команде разработки (веб, mobile, etc.) отдельно как работает ваш API, вам необходимо создать документацию к нему. 
В этом вам может помочь **Swagger**. Он описывает методы вашего WebApi, а также модели которые он использует. 
 
 Я буду обьяснять все на конкретном примере. 
 Для этого я создам ASP.NET Core 2.2 Web Application с шаблоном React.js в этом репозитории [SwaggerExample](https://github.com/d4n0n-myself/InformaticsSpring2019/tree/SwaggerExample/WebApplication5/SwaggerExample).
 
 Буду прикреплять коммиты по ходу статьи.
 
 # Installation
 Все изменения этого шага приведены здесь : [Swagger Initial Setup](https://github.com/d4n0n-myself/InformaticsSpring2019/commit/fe35d5c53c19ad0c1b3e0e91d68dd2811a344ff5)
 
В плане установки всё довольно просто:
 * загрузить nuGet пакет `Swashbuckle.AspNetCore` в проект вашего WebAPI. 
 * настроить Swagger для вашего приложения.
 
 Настроить Swagger стоит следующим образом: в классе `Startup.cs` дополнить метод `ConfigureServices` вызовом метода: 
 ```cs 
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("1.0.0", new OpenApiInfo()
    {
        Title = "d4n0n's API", 
        Version = "1.0.0",
        Contact = new OpenApiContact()
        {
            Email = "danon.sibaev@yandex.ru",
            Name = "d4n0n_myself"
        },
        Description = "Informatics Spring 2019 project. Uses ASP.NET Core MVC pattern."
    });
});
 ```
Наполнение лямбды может дополняться, если потребуется: вызовами методов с информацией о XML-файле с документацией и пр. Но обо всем по порядку. 

Для начала начнем с вызова метода, который хранит минимально необходимую информацию для Swagger : 
`options.SwaggerDoc`. Он принимает в себя два параметра: `name` и экземпляр `OpenApiInfo`, в котором следует указать св-во `Title` и `Version`. 
Конструктора у этого класса нет, поэтому все задается через фигурные скобки.

После этого, необходимо вызвать методы 
```cs
app.UseSwagger()
``` 
и 
```cs
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "d4n0n's API 1.0.0");
});
``` 
в методе `Startup.Configure`. 
В последнем методе в лямбду добавляем вызов метода SwaggerEndpoint, в который передаем URL, где будет хранится текущая конфигурация Swagger, и название конфигурации. 

Теперь Swagger доступен по ссылке : http://localhost:5000/swagger (если вы запускаете проект на localhost:5000).
Выглядит это примерно вот так - 
![Image1 on Drive](https://github.com/d4n0n-myself/InformaticsSpring2019/blob/SwaggerArticle/scrshts/1.png)


# Methods 

Все изменения приведены здесь: [Controllers and methods](https://github.com/d4n0n-myself/InformaticsSpring2019/commit/0f0126cc7c17badfb1234818928e4dc83babe479)

Чтобы беспрепятсвенно видеть все ваши контроллеры и методы в них, эти элементы должны быть **уникально описаны**. 

Для этого контроллеры описываются атрибутом `[Route]` из библиотеки `Microsoft.AspNetCore.Mvc`, а методы - атрибутами HTTP методов, таких как `[HttpGet]`. Swagger так же считает методы неуникальными, если описать методы атрибутами HTTP методов без параметра имени. Чтобы не замарачиваться с каждым названием метода для Swagger, можно использовать названия из C#, прикрепив их с помощью шаблона в атрибуте `Route` контроллера, как указано в моем примере. 

*Примечание* : при использовании атрибута `Route` с названиями методов и дополнительно указывая название в атрибутах HTTP около методов - будет наложение имен. В примере есть такой случай для метода `OmgItsASwagger` контроллера `SampleData`.

# Documentation

Все изменения приведены здесь: [XML](https://github.com/d4n0n-myself/InformaticsSpring2019/commit/909ab1ce520320beba296c00c07fb65bc46e5a32)

Я буду прикреплять документацию в качестве XML-файла, который сгенерируется при сборке проекта. Использовать такие файлы крайне рекомендуется не только ради документации в Swagger, но и повсеместно : некоторые IDE используют такие файлы и XML-комментарии для генерации справочной инфомации. (Например, ваши подсказки в ваших IDE при вызове метода, или использовании конструктора). 

Для того, чтобы добавить XML-документацию в Swagger, достаточно дописать одну строку в файл проекта и вызов метода в `AddSwargerGen`, который мы вызывали в `Startup.ConfigureServices`.

После этого, допишите ваши документации прямо в коде или в файле, на который вы укажете ссылку, и ваша документация поадет прямо в интерфейс Swagger.

*Примечание*: добавленная строчка в файл проекта укажет вам на публичные блоки кода, которые еще не были задокументированы, при сборке проекта. 

Должно получиться примерно следующее - 
![Image on Drive](https://github.com/d4n0n-myself/InformaticsSpring2019/blob/SwaggerArticle/scrshts/2.png)

# Conclusion

Таким образом, используя Swagger и час свободного времени, можно легко описать ваш готовый(!) WebAPI. 

### Источники вдохновения:
1. [Презентация с доклада на .NET Day](https://docs.google.com/presentation/d/1DpDwzdxFyFzOeopideqZqeLQ-cX5lTxDjPFXnxJiQy4/edit#slide=id.g50a090c05d_1_0)
2. [Репозиторий этого доклада](https://github.com/Krakaz/SwaggerExample)
3. [Обзор Swagger на docs.microsoft.com](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2)
4. [Настройка Swagger по docs.microsoft.com](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio)

# Optional : Customizaion

Кастомизацию можно добавить, внедрив в папку проекта wwwroot новую папку под именем swagger, и посместив в нее html, css + js.

Пример (просто чтобы был, никаких целей здесь не преследую) :  [Customization](https://github.com/d4n0n-myself/InformaticsSpring2019/commit/68b3e44dde97499f1bc7e4b36fa4c75382afabd7)
