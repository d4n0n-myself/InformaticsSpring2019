
# Introduction 
Допустим, вам заказали создать некое приложение.  
Заказчик потребовал несколько способов взаимодействия с приложением : мобильное приложение, веб-интерфейс или что-то другое.
Вы back-end разработчик и создали WebAPI для интегрирования front-end разработки в ваше приложение. 
Однако, чтобы не обьяснять каждой команде разработки (веб, mobile, etc.), вам необходимо создать документацию к вашему API. 
В этом вам может помочь Swagger.
 
 Я буду обьяснять все на конкретном примере. 
 Для этого я создам ASP.NET Core 2.2 Web Application с шаблоном React.js в этом репозитории [SwaggerExample]
 (https://github.com/d4n0n-myself/InformaticsSpring2019/tree/SwaggerExample/WebApplication5/SwaggerExample).
 
 Буду прикреплять коммиты по ходу статьи.
 
 # Installation
В плане установки всё довольно просто:
 * загрузить nuGet пакет Swashbuckle.AspNetCore в проект вашего WebAPI. 
 * настроить Swagger для вашего приложения.
 
 Настроить Swagger стоит следующим образом: в классе `Startup.cs` дополнить метод `ConfigureServices` вызовом метода: `services.AddSwaggerGen(options => *implementation*)`.
В качестве *implementation* я имею в виду вызовы методов для Swagger, которые позволяют дополнять его информацией, 
например: общей информацией, информацией о XML-файле с документацией и пр. Но обо всем по порядку. 

Для начала начнем с вызова метода, который хранит минимально необходимую информацию для Swagger : 
`options.SwaggerDoc`. Он принимает в себя два параметра: `name` и экземпляр `OpenApiInfo`, в котором следует указать св-во `Title` и `Version`. 
Конструктора у этого класса нет, поэтому все задается через фигурные скобки.

После этого, необходимо вызвать методы `app.UseSwagger()` и `app.UseSwaggerUI(options => *impl*)` в методе `Startup.Configure`. 
В последнем методе в лямбду следует добавить вызов метода SwaggerEndpoint, в который передать url, и название конфигурации. 

Все изменения этого шага приведены здесь : [Swagger Initial Setup](https://github.com/d4n0n-myself/InformaticsSpring2019/commit/fe35d5c53c19ad0c1b3e0e91d68dd2811a344ff5)

Теперь Swagger доступен по ссылке : http://localhost:5000/swagger (если вы запускаете проект на localhost:5000).
Выглядит это примерно вот так - [Image from Drive](https://drive.google.com/file/d/1_5r0ZdV4sAwMYiXuzPSfiyPTeD-WKxVU/)
