CREATE SCHEMA trello_bd;
SET search_path TO trello_bd, public;


CREATE TABLE IF NOT EXISTS users(
id_user UUID PRIMARY KEY DEFAULT gen_random_uuid(), --ver se a function do uuidv7() está habilitado no postgresql instalado
tx_name varchar(200) NOT NULL,
tx_email varchar(255) NOT NULL,
dt_created_at TIMESTAMP WITH TIME ZONE NOT NULL,
dt_updated_at TIMESTAMP WITH TIME ZONE,
fl_ativo bool default TRUE
);


CREATE TABLE IF NOT EXISTS credentials(
id_user UUID PRIMARY KEY DEFAULT gen_random_uuid(),
password_hash varchar(255) NOT NULL,
salt varchar(128) NOT NULL,
dt_password_updated_at TIMESTAMP WITH TIME ZONE,
CONSTRAINT FK_CREDENTIALS_USERS FOREIGN KEY(id_user) REFERENCES users(id_user)
);

CREATE TABLE IF NOT EXISTS boards(
id_board UUID PRIMARY KEY DEFAULT gen_random_uuid(),
tx_name varchar(200),
background_color varchar(30)
);

CREATE TABLE IF NOT EXISTS user_board(
id_board UUID,
id_user UUID,
fl_user_role varchar(40),
CONSTRAINT PK_USER_BOARD PRIMARY KEY(id_board, id_user),
CONSTRAINT FK_USER_BOARD_BOARD FOREIGN KEY(id_board) REFERENCES boards(id_board),
CONSTRAINT FK_USER_BOARD_USER FOREIGN KEY(id_user) REFERENCES users(id_user)
);

CREATE TABLE IF NOT EXISTS columns(
id_column UUID PRIMARY KEY DEFAULT gen_random_uuid(),
id_board UUID,
tx_title varchar(100),
nr_position smallint,
CONSTRAINT FK_COLUMN_BOARD FOREIGN KEY(id_board) REFERENCES boards(id_board)
);

CREATE TABLE task_item(
id_task_item UUID PRIMARY KEY DEFAULT gen_random_uuid(),
id_column UUID NOT NULL,
tx_title varchar(100),
tx_description text,
fl_completed bool,
dt_due_date timestamp with time zone,
nr_position smallint,
updated_at timestamp with time zone,
CONSTRAINT FK_TASK_ITEM_COLUMN FOREIGN KEY(id_column) REFERENCES columns(id_column)
);

CREATE TABLE IF NOT EXISTS task_item_user_history(
id_user UUID,
id_task_item UUID,
dt_action_date timestamp with time zone default NOW(),
tx_action text,
CONSTRAINT PK_TASK_ITEM_USER_HISTORY PRIMARY KEY(id_user, id_task_item),
CONSTRAINT FK_TASK_ITEM_USER_HISTORY_USERS FOREIGN KEY(id_user) REFERENCES users(id_user),
CONSTRAINT FK_TASK_ITEM_USER_HISTORY_TASK_ITEM FOREIGN KEY(id_task_item) REFERENCES task_item(id_task_item)
);

CREATE TABLE IF NOT EXISTS comments(
id_comment UUID PRIMARY KEY DEFAULT gen_random_uuid(),
id_user UUID,
id_task_item UUID,
tx_comment text,
tx_emojis text,   --ver como configurar o banco para que emojis possam ser encoded
dt_creation timestamp WITH TIME ZONE DEFAULT NOW(), --ver se deixar o timezone nas aplicações faz sentido
dt_last_modified timestamp WITH TIME ZONE,
fl_deleted bool DEFAULT FALSE,
CONSTRAINT FK_COMMENT_USERS FOREIGN KEY(id_user) REFERENCES users(id_user),
CONSTRAINT FK_COMMENT_TASK_ITEM FOREIGN KEY(id_task_item) REFERENCES task_item(id_task_item)
);

CREATE TABLE IF NOT EXISTS comment_history(
id_comment_history UUID PRIMARY KEY DEFAULT gen_random_uuid(),
id_user UUID,
id_comment UUID,
dt_action timestamp WITH TIME ZONE,
tx_action text,
CONSTRAINT FK_COMMENT_HISTORY_USERS FOREIGN KEY(id_user) REFERENCES users(id_user),
CONSTRAINT FK_COMMENT_HISTORY_COMMENTS FOREIGN KEY(id_comment) REFERENCES comments(id_comment)
);

