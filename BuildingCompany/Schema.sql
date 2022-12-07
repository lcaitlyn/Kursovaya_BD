create schema if not exists company;

create table if not exists company.position (
    id serial primary key,
    name varchar(256) unique not null ,
    salary int
);

create table if not exists company.customer (
    id serial primary key,
    name text not null ,
    phone text not null ,
    email text
);


create table if not exists company.brigade (
    id serial primary key ,
    name text not null,
    foremanId int references company.position(id)
);

create table if not exists company.employee (
    id serial primary key ,
    name text not null ,
    phone text not null ,
    brigadeId int references company.brigade(id),
    positionId int references company.position(id)
);

create table if not exists company.object (
    id serial primary key,
    name text not null ,
    address text not null ,
    startDate date not null,
    endDate date not null,
    brigadeId int references company.brigade(id)
);

create table if not exists company.contract (
    id serial primary key ,
    objectId int references object(id),
    customerId int references company.customer(id),
    salesmanId int references company.employee(id),
    date date not null ,
    price int not null default 0
);