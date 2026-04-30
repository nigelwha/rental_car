CREATE TYPE status_type AS ENUM ('ready', 'at_work', 'awaiting', 'cancelled');

CREATE TABLE Status (
    ID INT PRIMARY KEY,
    status status_type NOT NULL DEFAULT 'awaiting'
);