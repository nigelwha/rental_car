CREATE TABLE Rental_application (
	ID INT PRIMARY KEY,
	client_id INT,
	employees_id INT,
	FOREIGN KEY (client_id) REFERENCES Client(id),
	FOREIGN KEY (employees_id) REFERENCES Employees(id),
	information_the_equipment VARCHAR(500),
	date_of_creation DATE NOT NULL,
	closing_date DATE NOT NULL,
	status_id INT,
	FOREIGN KEY (status_id) REFERENCES Status(id)
);