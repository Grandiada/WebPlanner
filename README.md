# WebPlanner
web-приложение Event planning.
Есть административная часть и пользовательская часть.
Функции административной части:
Пользователь создает мероприятие произвольного типа с произвольным набором полей на определенную дату и время.
Функции пользовательской части:
Пользователь может зарегистрироваться на данное мероприятие.
-Зарегистрироваться можно посредством подтверждения ссылки на email
-Количество участников ограничено создателем мероприятия
-При входе в систему необходимо дополнить информацию с дополнительными полями о себе

Приложение на построено на базе ASP.net core 2.0
База данных MS SQL
ORM фреймворк EF core
реализована связь многие ко многим Event User для осуществления учета подписавшихся пользователей
Javascript фреймворк Jquery и вся работа с DOM осуществляется с его помощью
плагины Jquery: Jquery UI, fullCalendar
Для загрузки данных используется Ajax запросы данных в виде JSON
Для создания токена и работы с пользователями ASP.net identity
Отправка письма MailKit
Ограничение участников проверяется на клиентской части и на серверной
Ограничение доступа без ввода дополнительной информации реализовано фильтром InfoFilter
