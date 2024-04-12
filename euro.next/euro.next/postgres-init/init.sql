-- postgres-init/init.sql
DROP TABLE IF EXISTS weather_forecasts;
CREATE TABLE IF NOT EXISTS weather_forecasts (
    id SERIAL PRIMARY KEY,
    date TIMESTAMP NOT NULL,
    temp INT NOT NULL,
    result VARCHAR(255) NOT NULL
);
