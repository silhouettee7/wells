create table departments (
    id serial primary key,
    name varchar(100) not null unique,
    created_at timestamp default current_timestamp
);

create table wells (
    id serial primary key,
    name varchar(100) not null unique,
    department_id integer not null references departments(id) on delete cascade,
    commission_date date not null,
    created_at timestamp default current_timestamp
);

create table measurement_types (
    id serial primary key,
    name varchar(50) not null unique,
    unit varchar(20) not null,
    description text
);

create table measurements (
    id serial primary key,
    well_id integer not null references wells(id) on delete cascade,
    measurement_type_id integer not null references measurement_types(id),
    value decimal(10,4) not null,
    measurement_time timestamp not null,
    created_at timestamp default current_timestamp
);