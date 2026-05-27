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
INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('テスト太郎', 'taro@test.com', 1, 1);
INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('テスト花子', 'hanako@test.com', 1, 1);
INSERT INTO employee (name, email, emp_status_id, dep_id) VALUES ('テスト一郎', 'ichiro@test.com', 1, 1);


