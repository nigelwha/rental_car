CREATE TABLE Lease_agreement (
	ID INT PRIMARY KEY,
	conditions VARCHAR(50) NOT NULL,
	start_date DATE NOT NULL,
	end_date DATE NOT NULL,
	price DECIMAL(10, 2) DEFAULT 0.00,
	characteristics VARCHAR(500),
	technical_service_features VARCHAR(5000),
	payment_id INT,
	FOREIGN KEY (payment_id) REFERENCES Payment(id)
);