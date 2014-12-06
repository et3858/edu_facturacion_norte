/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     24/04/2013 11:23:53 am                       */
/*==============================================================*/


drop table if exists CLIENTE;

drop table if exists DETALLEFACTURA;

drop table if exists FACTURA;

drop table if exists PRODUCTO;

/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE
(
   CLIID                int not null,
   CLINOMBRE            char(15),
   CLIAPEPAT            char(15),
   CLIAPEMAT            char(15),
   CLITELEFONO          int,
   CLIEMAIL             varchar(80),
   primary key (CLIID)
);

/*==============================================================*/
/* Table: DETALLEFACTURA                                        */
/*==============================================================*/
create table DETALLEFACTURA
(
   DETFACTID            int not null,
   FACTID               int not null,
   PRODID               int not null,
   DETFACTCANTIDADALLEVAR int,
   DETFACTTOTALPORPRODUCTO int,
   primary key (DETFACTID)
);

/*==============================================================*/
/* Table: FACTURA                                               */
/*==============================================================*/
create table FACTURA
(
   FACTID               int not null,
   CLIID                int not null,
   FACTSUBTOTAL         int,
   FACTIVA              int,
   FACTTOTAL            int,
   FACTFECHAFACTURACION varchar(10),
   FACTHORAFACTURACION  varchar(8),
   FACTCONFIRMARCANCELACION bool,
   primary key (FACTID)
);

/*==============================================================*/
/* Table: PRODUCTO                                              */
/*==============================================================*/
create table PRODUCTO
(
   PRODID               int not null,
   PRODDESCRIPCION      varchar(80),
   PRODUNIDADES         int,
   PRODVALOR            int,
   primary key (PRODID)
);

alter table DETALLEFACTURA add constraint FK_ESTA_EN foreign key (PRODID)
      references PRODUCTO (PRODID) on delete restrict on update restrict;

alter table DETALLEFACTURA add constraint FK_TIENE_EN foreign key (FACTID)
      references FACTURA (FACTID) on delete restrict on update restrict;

alter table FACTURA add constraint FK_PUEDE_TENER foreign key (CLIID)
      references CLIENTE (CLIID) on delete restrict on update restrict;

