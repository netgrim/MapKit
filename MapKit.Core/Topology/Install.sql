CREATE TABLE "Feature" (
	"id" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , 
	"name" TEXT UNIQUE )

insert into feature (name) 
select from_id from connectivity 
union 
select to_id from connectivity
union
select chan_id from connectivity where chan_id <> '0'

--insert missing dev_int_info
select * 
from 
 (select dev_def_id, pnt_id_a pnt_id from dev_int_conn
  union 
  select dev_def_id, pnt_id_b from dev_int_conn) dic
where not exists (Select * from dev_int_info dii where dii.dev_def_id = dic.dev_def_id and dii.inf_id = dic.pnt_id)


CREATE UNIQUE INDEX IDX_NAME ON feature  (NAME ASC  )

CREATE TABLE "edge" (
"id" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , 
FID INTEGER references feature(id) on delete cascade,
"node1_id" INTEGER, 
"node2_id" INTEGER, 
"phase" TEXT, 
"state" INTEGER, 
"direction" INTEGER)

CREATE INDEX IDX_NODE1_ID ON EDGE(NODE1_ID ASC);
CREATE INDEX IDX_NODE2_ID ON EDGE(NODE2_ID ASC);
CREATE INDEX IDX_FID ON EDGE(FID ASC);

CREATE TABLE FEATURE_NODE
    (FIDENTITY_ID                      NUMBER NOT NULL,
    PNT_ID                         VARCHAR2(30) NOT NULL,
    NODE_ID                        NUMBER NOT NULL
  ,
  CONSTRAINT PK_ENTITY_NODE
  PRIMARY KEY (ENTITY_ID, PNT_ID)
  USING INDEX)
/