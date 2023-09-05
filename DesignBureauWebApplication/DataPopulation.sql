---- Вставляем тестовые данные в таблицу Department
--IF NOT EXISTS (SELECT * FROM Department)
--	INSERT INTO Department (DepartmentTitle, LocationId) VALUES
--	('Отдел разработки и интеграции роботизированных систем', 1),
--	('Отдел производства и монтажа промышленных роботов', 2),
--	('Отдел маркетинга и продаж роботизированных решений', 3);

---- Вставляем тестовые данные в таблицу Employee
--IF NOT EXISTS (SELECT * FROM Employee)
--	INSERT INTO Employee (ELastName, EFirstName, EPatronymic, BirthDate, Email, PhoneNumber, PositionId) VALUES
--	('Иванов', 'Алексей', 'Петрович', '1985-06-12', 'ivanov@robotics.ru', '+7 495 123 45 67', 1),
--	('Смирнова', 'Елена', 'Викторовна', '1990-03-25', 'smirnova@robotics.ru', '+7 495 234 56 78', 2),
--	('Петров', 'Дмитрий', 'Андреевич', '1988-10-15', 'petrov@robotics.ru', '+7 495 345 67 89', 3);

---- Вставляем тестовые данные в таблицу Interaction
--IF NOT EXISTS (SELECT * FROM Interaction)
--	INSERT INTO Interaction (CreatedAt, InteractionStatus, WorkId, InteractionDictionaryId, EmployeeId, InventoryId) VALUES
--	('2023-03-11 10:00:00', 1, 1, 1, 1, 1),
--	('2023-03-11 11:30:00', 2, 2, 2, 2, 2),
--	('2023-03-11 12:15:00', 3, 3, 3, 3, 3);

---- Вставляем тестовые данные в таблицу InteractionDictionary
--IF NOT EXISTS (SELECT * FROM InteractionDictionary)
--	INSERT INTO InteractionDictionary (InteractionTitle) VALUES
--	('Проверка состояния робота'),
--	('Замена деталей робота'),
--	('Программирование робота');

---- Вставляем тестовые данные в таблицу Inventory
--IF NOT EXISTS (SELECT * FROM Inventory)
--	INSERT INTO Inventory (InventoryName, InventoryType) VALUES
--	('Робот-пылесос', 1),
--	('Аккумулятор', 2),
--	('Компьютер', 3);

---- Вставляем тестовые данные в таблицу InventoryOrder
--IF NOT EXISTS (SELECT * FROM InventoryOrder)
--	INSERT INTO InventoryOrder (Cost, PlanDeliveryDateTime, OrderStatus, InventoryId, OrderId) VALUES
--	(10000, '2023-03-15 12:00:00', 1, 1, 1),
--	(5000, '2023-03-16 10:00:00', 2, 2, 2),
--	(20000, '2023-03-17 14:00:00', 3, 3, 3);

---- Вставляем тестовые данные в таблицу InventoryTransportation
--IF NOT EXISTS (SELECT * FROM InventoryTransportation)
--	INSERT INTO InventoryTransportation (InventoryId, TransportationId, TransportationStatus) VALUES
--	(1, 1, 1),
--	(2, 2, 2),
--	(3, 3, 3);

---- Вставляем тестовые данные в таблицу Location
--IF NOT EXISTS (SELECT * FROM "Location")
--	INSERT INTO "Location" ("Address", LocationType) VALUES
--	('Москва, ул. Ленина, д. 1', 1),
--	('Санкт-Петербург, наб. Мойки, д. 2', 2),
--	('Новосибирск, пр. Карла Маркса, д. 3', 3);

---- Вставляем тестовые данные в таблицу Order
--IF NOT EXISTS (SELECT * FROM "Order")
--	INSERT INTO "Order" (CreatedAt, ContractNumber, SupplierId) VALUES
--	('2023-03-01 10:00:00', '1234567890', 1),
--	('2023-03-02 11:00:00', '2345678901', 2),
--	('2023-03-03 12:00:00', '3456789012', 3);

---- Вставляем тестовые данные в таблицу Position
--IF NOT EXISTS (SELECT * FROM Position)
--	INSERT INTO Position (JobTitle, DepartmentId) VALUES
--	('Главный инженер', 1),
--	('Ведущий разработчик', 2),
--	('Младший разработчик', 2),
--	('Тестировщик', 3),
--	('Аналитик', 1),
--	('Менеджер проекта', 3);

---- Вставляем тестовые данные в таблицу Project
--IF NOT EXISTS (SELECT * FROM Project)
--	INSERT INTO Project (ProjectTitle, PlanStartDate, PlanOverDate) VALUES
--	('Разработка робота-помощника для дома', '2023-01-01', '2023-12-31'),
--	('Разработка робота-охранника для банка', '2023-02-01', '2023-10-31'),
--	('Разработка робота-учителя для школы', '2023-03-01', '2024-02-28'),
--	('Разработка робота-врача для больницы', '2023-04-01', '2024-06-30'),
--	('Разработка робота-повара для ресторана', '2023-05-01', '2024-04-30');

---- Вставляем тестовые данные в таблицу Supplier
--IF NOT EXISTS (SELECT * FROM Supplier)
--	INSERT INTO Supplier (SName, PhoneNumber, Email) VALUES
--	('Роботех', '+7 (495) 123-45-67', 'robotekh@mail.ru'),
--	('Электроника', '+7 (499) 765-43-21', 'electronika@gmail.com'),
--	('Механика', '+7 (812) 456-78-90', 'mehanika@yandex.ru'),
--	('Сенсорика', '+7 (831) 987-65-43', 'sensorika@outlook.com'),
--	('Программистика', '+7 (383) 654-32-10', 'programmistika@bk.ru');

---- Вставляем тестовые данные в таблицу Transportation
--IF NOT EXISTS (SELECT * FROM Transportation)
--	INSERT INTO Transportation (PlanDepartureDateTime, PlanArrivalDateTime, CreatedAt, TransportationType, OriginId, DestinationId) VALUES
--	('2023-01-15 10:00:00', '2023-01-15 12:00:00', '2022-12-31 15:00:00', 0, 1, 2),
--	('2023-02-01 08:00:00', '2023-02-01 10:30:00', '2023-01-15 16:00:00', NULL, 2, 3),
--	('2023-03-10 09:30:00', '2023-03-10 11:45:00', '2023-02-28 17:30:00', NULL ,2 ,1),
--	('2023-04-20 11:15:00', '2023-04-20 13:30:00', '2023-04-01 18:45:00' ,1 , 3, 2),
--	('2023-05-25 12:45:00', '2023-05-01 19:15:00', '2023-04-01 18:45:00', NULL, 1, 2);

---- Вставляем тестовые данные в таблицу Work
--IF NOT EXISTS (SELECT * FROM Work)
--	INSERT INTO Work (WorkNumber, PlanStartDateTime, PlanOverDateTime, ProjectId, WorkDictionaryId, PreviousWorkId, NextWorkId) VALUES
--	(1, '2023-01-01 10:00:00', '2023-01-02 12:00:00', 1, 1, NULL, NULL);
--	INSERT INTO Work (WorkNumber, PlanStartDateTime, PlanOverDateTime, ProjectId, WorkDictionaryId, PreviousWorkId, NextWorkId) VALUES
--	(2, '2023-01-02 13:00:00', '2023-01-03 15:00:00', 1, 2, NULL, NULL);
--	INSERT INTO Work (WorkNumber, PlanStartDateTime, PlanOverDateTime, ProjectId, WorkDictionaryId, PreviousWorkId, NextWorkId) VALUES
--	(3, '2023-01-03 16:00:00', '2023-01-04 18:00:00', 1, 3, NULL, NULL);
--	INSERT INTO Work (WorkNumber, PlanStartDateTime, PlanOverDateTime, ProjectId, WorkDictionaryId, PreviousWorkId, NextWorkId) VALUES
--	(1, '2023-02-01 08:30:00', '2023-02-02 10:30:00', 2, 4, NULL, NULL);
--	INSERT INTO Work (WorkNumber, PlanStartDateTime, PlanOverDateTime, ProjectId, WorkDictionaryId, PreviousWorkId, NextWorkId) VALUES
--	(2, '2023-02-02 11:30:00', '2023-02-03 13:30:00', 2, 5, NULL, NULL);

---- Вставляем тестовые данные в таблицу WorkDictionary
--IF NOT EXISTS (SELECT * FROM WorkDictionary)
--	INSERT INTO WorkDictionary (WorkTitle) VALUES
--	('Разработка концепции'),
--	('Создание прототипа'),
--	('Тестирование и отладка'),
--	('Дизайн и верстка'),
--	('Написание документации');