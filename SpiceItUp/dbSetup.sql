CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title TEXT NOT NULL,
  subtitle TEXT NOT NULL,
  category TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS ingredients(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  quantity INT NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS steps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  step INT NOT NULL,
  body TEXT NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS favorites(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO recipes
(title, subtitle, category, creatorId)
VALUES
("First Recipe", "First Subtitle", "First Category", "619afeeffa6f61641d753992");

INSERT INTO ingredients
(name, quantity, recipeId)
VALUES
("First Recipe", 5, 2);
