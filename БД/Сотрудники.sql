CREATE TABLE Employees (
	ID INT PRIMARY KEY,
	surname VARCHAR(50) NOT NULL,
	name VARCHAR(50) NOT NULL,
	middle_name VARCHAR(50) NULL,
	job_title VARCHAR(50) NOT NULL,
	mail VARCHAR(50) NOT NULL,
	phone_number VARCHAR(50) NOT NULL,
	document_id INT,
	FOREIGN KEY (document_id) REFERENCES Documents(id)
);