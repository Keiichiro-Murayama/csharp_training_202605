TRUNCATE TABLE
    employee,
    department,
    empstatus
RESTART IDENTITY CASCADE;

INSERT INTO department (name) VALUES ('人事部');
INSERT INTO department (name) VALUES ('総務部');
INSERT INTO department (name) VALUES ('経理部');


INSERT INTO empstatus (name) VALUES('正社員');
INSERT INTO empstatus (name) VALUES('契約社員');
INSERT INTO empstatus (name) VALUES('アルバイト');
INSERT INTO empstatus (name) VALUES('役員');

INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('渡辺謙', 'ken@actors.com', 1, 1);
INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('阿部寛', 'hiroshi@actors.com', 2, 2);
INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('河口春奈', 'haruna@actors.com', 3, 3);


