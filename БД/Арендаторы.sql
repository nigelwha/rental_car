CREATE TABLE Client (
	ID INT PRIMARY KEY,
	surname VARCHAR(50) NOT NULL,
	name VARCHAR(50) NOT NULL,
	middle_name VARCHAR(50),
	company_name VARCHAR(50),
	email VARCHAR(50),
	phone_number VARCHAR(20),
	rental_history VARCHAR(500),
	service_history VARCHAR(500),
	equipment_id INT,
	lease_agreement_id INT,
	FOREIGN KEY (equipment_id) REFERENCES Equipment(id),
	FOREIGN KEY (lease_agreement_id) REFERENCES Lease_agreement(id)
);