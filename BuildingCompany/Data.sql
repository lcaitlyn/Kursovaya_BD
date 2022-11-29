insert into company.position (name, salary)
    value ('Маляр', 2200);
insert into company.position (name, salary)
    value ('Сварщик', 2300);
insert into company.position (name, salary)
    value ('Разнорабочий', 1500);
insert into company.position (name, salary)
    value ('Сантехник', 1800);
insert into company.position (name, salary)
    value ('Электрик', 1900);
insert into company.position (name, salary)
    value ('Бригадир', 2500);
insert into company.position (name, salary)
    value ('Менеджер по продажам', 2350);

insert into company.employee (name, phone, positionId)
    value ('Высоцкий Тимофей Кириллович', '+7(495)615-54-46',
           (select id from company.position where name='Бригадир'));
insert into company.employee (name, phone, positionId)
    value ('Орлов Али Михайлович', '+7(495)017-17-54',
           (select id from company.position where name='Бригадир'));
insert into company.employee (name, phone, positionId)
    value ('Кузин Михаил Даниилович', '+7(495)806-82-73',
           (select id from company.position where name='Бригадир'));

insert into company.brigade (name, foremanId)
    value ('Бригада 1', 1);
insert into company.brigade (name, foremanId)
    value ('Бригада 2', 2);
insert into company.brigade (name, foremanId)
    value ('Бригада 3', 3);

update company.employee set brigadeId=1 where id=1;
update company.employee set brigadeId=2 where id=2;
update company.employee set brigadeId=3 where id=3;

insert into company.employee (name, phone, positionId)
    value ('Олейникова Полина Григорьевна', '+7(495)923-42-10',
          (select id from company.position where name='Менеджер по продажам'));

insert into company.employee (name, phone, brigadeId, positionId)
    value ('Сычева Мария Данииловна', '+7(495)524-48-10', 1,
           (select id from company.position where name='Маляр'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Михайлов Никита Юрьевич', '+7(495)432-65-38', 2,
           (select id from company.position where name='Маляр'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Семенов Александр Вячеславович', '+7(495)363-54-76', 3,
           (select id from company.position where name='Маляр'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Сидоров Сергей Тихонович', '+7(495)798-96-67', 1,
           (select id from company.position where name='Сварщик'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Козлова Ева Руслановна', '+7(495)916-73-75', 2,
           (select id from company.position where name='Сварщик'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Леонтьев Константин Захарович', '+7(495)174-60-25', 3,
           (select id from company.position where name='Сварщик'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Гусева Александра Михайловна', '+7(495)999-36-65', 1,
           (select id from company.position where name='Разнорабочий'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Гусев Мирон Степанович', '+7(495)543-20-13', 2,
           (select id from company.position where name='Разнорабочий'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Гончарова Дарья Романовна', '+7(495)307-67-24', 3,
           (select id from company.position where name='Разнорабочий'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Морозова Вера Львовна', '+7(495)995-01-05', 1,
           (select id from company.position where name='Сантехник'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Фролов Александр Ярославович', '+7(495)045-61-00', 2,
           (select id from company.position where name='Сантехник'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Андреев Захар Алексеевич', '+7(495)449-56-35', 3,
           (select id from company.position where name='Сантехник'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Никитин Иван Максимович', '+7(495)326-15-10', 1,
           (select id from company.position where name='Электрик'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Родионов Иван Максимович', '+7(495)055-26-03', 2,
           (select id from company.position where name='Электрик'));
insert into company.employee (name, phone, brigadeId, positionId)
    value ('Капустин Лев Ярославович', '+7(495)583-69-43', 3,
           (select id from company.position where name='Электрик'));

insert into company.customer (name, phone, email)
    value ('Курочкин Вадим Миронович', '+7(495)150-51-65', 'kurochkin_va_dim@mail.ru');
insert into company.customer (name, phone, email)
    value ('Котов Иван Русланович', '+7(495)485-95-35', 'ivan_pussy@gmail.com');
insert into company.customer (name, phone, email)
    value ('Михайлов Борис Артурович', '+7(495)224-71-39', 'boris_the_blade@yandex.ru');

insert into company.object (name, address, startDate, endDate, brigadeId)
    value ('Коттедж', 'Кооперативная ул., 29, Казань, Респ. Татарстан, 420101', '2005-12-25', '2015-5-13', 1);
insert into company.object (name, address, startDate, endDate, brigadeId)
    value ('Летний домик', 'ул. Героев Хасана, 23, Казань, Респ. Татарстан, 420101', '2016-6-10', '2020-2-17', 2);
insert into company.object (name, address, startDate, endDate, brigadeId)
    value ('Вилла', 'ул. Матур, 4, Казань, Респ. Татарстан, 420138', '2013-9-1', '2018-8-19', 3);

insert into company.contract (objectId, customerId, salesmanId, date, price)
    value (1, 1, 4, '2013-6-15', 5500000);
insert into company.contract (objectId, customerId, salesmanId, date, price)
    value (2, 2, 4, '2020-3-3', 6400000);
insert into company.contract (objectId, customerId, salesmanId, date, price)
    value (3, 3, 4, '2018-9-1', 7900000);
