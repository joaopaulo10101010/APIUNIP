create database db_unip;

use db_unip;

create table TB_LDR(
	LDR_CODIGO int auto_increment primary key,
    LDR_HORADOREGISTRO datetime default current_timestamp,
    LDR_VALORCOLETADO double default 0,
    LDR_VALORPROCESSADO double default 0,
    LDR_STATUS varchar(50)
);

create table TB_MQ135(
	MQ135_CODIGO int auto_increment primary key,
    MQ135_HORADOREGISTRO datetime default current_timestamp,
    MQ135_VALORCOLETADO double default 0,
    MQ135_VALORPROCESSADO double default 0,
    MQ135_STATUS varchar(50)
);

create table TB_BMP280(
	BMP280_CODIGO int auto_increment primary key,
    BMP280_HORADOREGISTRO datetime default current_timestamp,
    BMP280_VALORCOLETADO double default 0,
    BMP280_VALORPROCESSADO double default 0,
    BMP280_STATUS varchar(50)
);

create table TB_DHT22(
	DHT22_CODIGO int auto_increment primary key,
    DHT22_HORADOREGISTRO datetime default current_timestamp,
    DHT22_VALORCOLETADO double default 0,
    DHT22_VALORPROCESSADO double default 0,
    DHT22_STATUS varchar(50)
);
