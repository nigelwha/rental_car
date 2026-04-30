CREATE TABLE Equipment (
	ID INT PRIMARY KEY,
	manufacturer VARCHAR(50),
	model VARCHAR(50),
	characteristics VARCHAR(500),
	state VARCHAR(50),
	price DECIMAL(10, 2) DEFAULT 0.00,
	service_date DATE
);