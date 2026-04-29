================================================================
-- 1. Справочники
-- ================================================================
CREATE TABLE roles (
    role_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE car_statuses (
    status_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE reservation_statuses (
    status_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

-- ================================================================
-- 2. Пользователи и авторизация
-- ================================================================
CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50),
    birth_date DATE NOT NULL,
    phone VARCHAR(15) NOT NULL,
    email VARCHAR(100),
    address VARCHAR(200),
    driver_license_number VARCHAR(20),
    license_issue_date DATE,
    license_category VARCHAR(5) DEFAULT 'B',
    role_id INT NOT NULL REFERENCES roles(role_id),
    employee_code VARCHAR(20) UNIQUE,
    hire_date DATE,
    registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE authorizations (
    auth_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL UNIQUE REFERENCES users(user_id) ON DELETE CASCADE,
    login VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

-- ================================================================
-- 3. Марки и модели
-- ================================================================
CREATE TABLE brands (
    brand_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    country VARCHAR(50)
);

CREATE TABLE models (
    model_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    brand_id INT NOT NULL REFERENCES brands(brand_id),
    class VARCHAR(20)
);

-- ================================================================
-- 4. Автомобили
-- ================================================================
CREATE TABLE cars (
    car_id SERIAL PRIMARY KEY,
    model_id INT NOT NULL REFERENCES models(model_id),
    license_plate VARCHAR(9) NOT NULL UNIQUE,
    manufacture_year INT NOT NULL CHECK (manufacture_year BETWEEN 1990 AND EXTRACT(YEAR FROM CURRENT_DATE) + 1),
    mileage INT NOT NULL DEFAULT 0,
    status_id INT NOT NULL REFERENCES car_statuses(status_id) DEFAULT 1,
    base_rental_rate_per_day DECIMAL(10,2) NOT NULL,
    color VARCHAR(20)
);

-- ================================================================
-- 5. Бронирование и аренда
-- ================================================================
CREATE TABLE reservations (
    reservation_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(user_id),
    car_id INT NOT NULL REFERENCES cars(car_id),
    start_date DATE NOT NULL,
    end_date DATE NOT NULL CHECK (end_date >= start_date),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status_id INT NOT NULL REFERENCES reservation_statuses(status_id) DEFAULT 1,
    prepayment_amount DECIMAL(10,2) NOT NULL,
    estimated_total_cost DECIMAL(10,2) NOT NULL
);

CREATE TABLE rentals (
    rental_id SERIAL PRIMARY KEY,
    reservation_id INT REFERENCES reservations(reservation_id) ON DELETE SET NULL,
    user_id INT NOT NULL REFERENCES users(user_id),
    car_id INT NOT NULL REFERENCES cars(car_id),
    pickup_date TIMESTAMP NOT NULL,
    expected_return_date DATE NOT NULL,
    actual_return_date TIMESTAMP,
    mileage_at_pickup INT NOT NULL,
    mileage_at_return INT,
    rental_period INT,                    -- дней (можно вычислять, но пусть будет)
    return_condition TEXT,
    final_cost DECIMAL(10,2)
);

-- ================================================================
-- 6. Платежи и штрафы
-- ================================================================
CREATE TABLE payments (
    payment_id SERIAL PRIMARY KEY,
    reservation_id INT REFERENCES reservations(reservation_id),
    rental_id INT REFERENCES rentals(rental_id),
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    amount DECIMAL(10,2) NOT NULL,
    operation_type VARCHAR(20) NOT NULL CHECK (operation_type IN ('Prepayment', 'Main Payment', 'Additional Payment', 'Refund')),
    payment_method VARCHAR(20) NOT NULL,
    CONSTRAINT chk_payment_link CHECK (
        (reservation_id IS NOT NULL AND rental_id IS NULL) OR
        (reservation_id IS NULL AND rental_id IS NOT NULL)
    )
);

CREATE TABLE fines (
    fine_id SERIAL PRIMARY KEY,
    rental_id INT NOT NULL REFERENCES rentals(rental_id),
    issued_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    reason VARCHAR(50) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    status VARCHAR(20) DEFAULT 'Issued' CHECK (status IN ('Issued', 'Paid', 'Disputed'))
);

-- ================================================================
-- 7. Обслуживание
-- ================================================================
CREATE TABLE maintenance_records (
    maintenance_id SERIAL PRIMARY KEY,
    car_id INT NOT NULL REFERENCES cars(car_id),
    work_description VARCHAR(100) NOT NULL,
    service_date DATE NOT NULL,
    cost DECIMAL(10,2) NOT NULL
);

-- ================================================================
-- Индексы
-- ================================================================
CREATE INDEX idx_rentals_car_dates ON rentals(car_id, pickup_date, actual_return_date);
CREATE INDEX idx_reservations_dates ON reservations(start_date, end_date);
CREATE INDEX idx_cars_status ON cars(status_id);

