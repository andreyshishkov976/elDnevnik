-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.3.16-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Дамп структуры базы данных school
CREATE DATABASE IF NOT EXISTS `school` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `school`;

-- Дамп структуры для таблица school.auditorii
CREATE TABLE IF NOT EXISTS `auditorii` (
  `id_auditorii` int(11) NOT NULL AUTO_INCREMENT,
  `nom_auditorii` varchar(5) NOT NULL,
  PRIMARY KEY (`id_auditorii`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы school.auditorii: ~9 rows (приблизительно)
/*!40000 ALTER TABLE `auditorii` DISABLE KEYS */;
INSERT IGNORE INTO `auditorii` (`id_auditorii`, `nom_auditorii`) VALUES
	(1, '1-1'),
	(2, '1-2'),
	(3, '1-3'),
	(4, '1-4'),
	(5, '1-5'),
	(6, '1-6'),
	(7, '1-7'),
	(8, '2-1'),
	(9, '2-2');
/*!40000 ALTER TABLE `auditorii` ENABLE KEYS */;

-- Дамп структуры для таблица school.fakultativy
CREATE TABLE IF NOT EXISTS `fakultativy` (
  `id_fakultativa` int(11) NOT NULL AUTO_INCREMENT,
  `id_predmeta` int(11) NOT NULL,
  `id_prepod` int(11) NOT NULL,
  `date_provedeniya` date NOT NULL,
  `id_auditorii` int(11) NOT NULL,
  `time_n` time NOT NULL,
  `time_k` time NOT NULL,
  PRIMARY KEY (`id_fakultativa`),
  KEY `id_predmeta` (`id_predmeta`),
  KEY `id_prepod` (`id_prepod`),
  KEY `id_auditorii` (`id_auditorii`),
  CONSTRAINT `fakultativy_ibfk_1` FOREIGN KEY (`id_auditorii`) REFERENCES `auditorii` (`id_auditorii`),
  CONSTRAINT `fakultativy_ibfk_2` FOREIGN KEY (`id_predmeta`) REFERENCES `predmety` (`id_predmeta`),
  CONSTRAINT `fakultativy_ibfk_3` FOREIGN KEY (`id_prepod`) REFERENCES `prepod` (`id_prepod`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Факультативные занятия';

-- Дамп данных таблицы school.fakultativy: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `fakultativy` DISABLE KEYS */;
INSERT IGNORE INTO `fakultativy` (`id_fakultativa`, `id_predmeta`, `id_prepod`, `date_provedeniya`, `id_auditorii`, `time_n`, `time_k`) VALUES
	(2, 1, 1, '2020-03-02', 1, '19:35:00', '20:35:00');
/*!40000 ALTER TABLE `fakultativy` ENABLE KEYS */;

-- Дамп структуры для таблица school.homework
CREATE TABLE IF NOT EXISTS `homework` (
  `id_homework` int(11) NOT NULL AUTO_INCREMENT,
  `id_zanyatiya` int(11) NOT NULL,
  `zadanie` varchar(300) NOT NULL,
  `date` date NOT NULL,
  `id_uroka` int(11) NOT NULL,
  PRIMARY KEY (`id_homework`),
  KEY `id_zanyatiya` (`id_zanyatiya`),
  KEY `id_uroka` (`id_uroka`),
  CONSTRAINT `homework_ibfk_1` FOREIGN KEY (`id_zanyatiya`) REFERENCES `zanyatiya` (`id_zanyatiya`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `homework_ibfk_2` FOREIGN KEY (`id_uroka`) REFERENCES `uroki` (`id_uroka`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Домашнее задание';

-- Дамп данных таблицы school.homework: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `homework` DISABLE KEYS */;
INSERT IGNORE INTO `homework` (`id_homework`, `id_zanyatiya`, `zadanie`, `date`, `id_uroka`) VALUES
	(2, 3, 'ДЗ для 04 сентября 2020', '2020-09-04', 14),
	(3, 4, 'ДЗ для 05 сентября 2020', '2020-09-05', 14),
	(4, 5, 'ДЗ для 14 ноября 2020', '2020-11-14', 10),
	(5, 6, 'ДЗ для 16 ноября 2020', '2020-11-16', 10);
/*!40000 ALTER TABLE `homework` ENABLE KEYS */;

-- Дамп структуры для таблица school.klassy
CREATE TABLE IF NOT EXISTS `klassy` (
  `id_klassa` int(11) NOT NULL AUTO_INCREMENT,
  `nom_klassa` int(2) NOT NULL,
  `parallel` varchar(1) NOT NULL,
  `kolvo_uch` int(2) NOT NULL,
  PRIMARY KEY (`id_klassa`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='Справочник классов';

-- Дамп данных таблицы school.klassy: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `klassy` DISABLE KEYS */;
INSERT IGNORE INTO `klassy` (`id_klassa`, `nom_klassa`, `parallel`, `kolvo_uch`) VALUES
	(1, 1, 'А', 20),
	(2, 2, 'А', 20),
	(3, 1, 'Б', 20),
	(4, 2, 'Б', 20);
/*!40000 ALTER TABLE `klassy` ENABLE KEYS */;

-- Дамп структуры для таблица school.otmetki
CREATE TABLE IF NOT EXISTS `otmetki` (
  `id_otmetki` int(11) NOT NULL AUTO_INCREMENT,
  `id_uchenika` int(11) NOT NULL,
  `id_zanyatiya` int(11) NOT NULL,
  `znachenie` varchar(2) NOT NULL DEFAULT '',
  PRIMARY KEY (`id_otmetki`),
  KEY `id_uchenika` (`id_uchenika`),
  KEY `id_zanyatiya` (`id_zanyatiya`),
  CONSTRAINT `otmetki_ibfk_1` FOREIGN KEY (`id_uchenika`) REFERENCES `ucheniki` (`id_uchenika`),
  CONSTRAINT `otmetki_ibfk_2` FOREIGN KEY (`id_zanyatiya`) REFERENCES `zanyatiya` (`id_zanyatiya`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COMMENT='Отметки учащихся';

-- Дамп данных таблицы school.otmetki: ~8 rows (приблизительно)
/*!40000 ALTER TABLE `otmetki` DISABLE KEYS */;
INSERT IGNORE INTO `otmetki` (`id_otmetki`, `id_uchenika`, `id_zanyatiya`, `znachenie`) VALUES
	(1, 1, 3, '6'),
	(2, 1, 5, '5'),
	(3, 2, 3, '4'),
	(4, 2, 5, '8'),
	(5, 1, 4, '7'),
	(6, 2, 4, '6'),
	(7, 1, 6, '9'),
	(8, 2, 6, '');
/*!40000 ALTER TABLE `otmetki` ENABLE KEYS */;

-- Дамп структуры для таблица school.predmety
CREATE TABLE IF NOT EXISTS `predmety` (
  `id_predmeta` int(11) NOT NULL AUTO_INCREMENT,
  `naimenovanie` varchar(150) NOT NULL,
  PRIMARY KEY (`id_predmeta`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='Справочник предметов';

-- Дамп данных таблицы school.predmety: ~7 rows (приблизительно)
/*!40000 ALTER TABLE `predmety` DISABLE KEYS */;
INSERT IGNORE INTO `predmety` (`id_predmeta`, `naimenovanie`) VALUES
	(1, 'Русский язык'),
	(2, 'Математика'),
	(3, 'Белорусский язык'),
	(4, 'Физика'),
	(5, 'Информатика'),
	(6, 'История Беларуси'),
	(7, 'Всемирная история');
/*!40000 ALTER TABLE `predmety` ENABLE KEYS */;

-- Дамп структуры для таблица school.prepod
CREATE TABLE IF NOT EXISTS `prepod` (
  `id_prepod` int(11) NOT NULL AUTO_INCREMENT,
  `familiya` varchar(50) NOT NULL,
  `imya` varchar(50) NOT NULL,
  `otchestvo` varchar(50) NOT NULL,
  `id_predmeta` int(11) NOT NULL,
  `login` varchar(18) NOT NULL,
  `parol` varchar(18) NOT NULL,
  PRIMARY KEY (`id_prepod`),
  KEY `id_predmeta` (`id_predmeta`),
  CONSTRAINT `prepod_ibfk_1` FOREIGN KEY (`id_predmeta`) REFERENCES `predmety` (`id_predmeta`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Справочник преподавателей';

-- Дамп данных таблицы school.prepod: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `prepod` DISABLE KEYS */;
INSERT IGNORE INTO `prepod` (`id_prepod`, `familiya`, `imya`, `otchestvo`, `id_predmeta`, `login`, `parol`) VALUES
	(1, 'Петров', 'Иван', 'Васильевич', 1, '14881337', 'petrov1337');
/*!40000 ALTER TABLE `prepod` ENABLE KEYS */;

-- Дамп структуры для таблица school.raspisanie
CREATE TABLE IF NOT EXISTS `raspisanie` (
  `id_raspisaniya` int(11) NOT NULL AUTO_INCREMENT,
  `id_klassa` int(11) NOT NULL,
  `den_nedeli` varchar(11) NOT NULL,
  PRIMARY KEY (`id_raspisaniya`),
  KEY `id_klassa` (`id_klassa`),
  CONSTRAINT `raspisanie_ibfk_1` FOREIGN KEY (`id_klassa`) REFERENCES `klassy` (`id_klassa`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8 COMMENT='Расписание уроков';

-- Дамп данных таблицы school.raspisanie: ~5 rows (приблизительно)
/*!40000 ALTER TABLE `raspisanie` DISABLE KEYS */;
INSERT IGNORE INTO `raspisanie` (`id_raspisaniya`, `id_klassa`, `den_nedeli`) VALUES
	(1, 1, 'Вторник'),
	(4, 1, 'Пятница'),
	(7, 1, 'Понедельник'),
	(8, 1, 'Суббота');
/*!40000 ALTER TABLE `raspisanie` ENABLE KEYS */;

-- Дамп структуры для таблица school.ucheniki
CREATE TABLE IF NOT EXISTS `ucheniki` (
  `id_uchenika` int(11) NOT NULL AUTO_INCREMENT,
  `familiya` varchar(50) NOT NULL,
  `imya` varchar(50) NOT NULL,
  `otchestvo` varchar(50) NOT NULL,
  `id_klassa` int(11) NOT NULL,
  `login` varchar(18) NOT NULL,
  `parol` varchar(18) NOT NULL,
  PRIMARY KEY (`id_uchenika`),
  KEY `id_klassa` (`id_klassa`),
  CONSTRAINT `ucheniki_ibfk_1` FOREIGN KEY (`id_klassa`) REFERENCES `klassy` (`id_klassa`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Справочник учащихся';

-- Дамп данных таблицы school.ucheniki: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `ucheniki` DISABLE KEYS */;
INSERT IGNORE INTO `ucheniki` (`id_uchenika`, `familiya`, `imya`, `otchestvo`, `id_klassa`, `login`, `parol`) VALUES
	(1, 'Сидорова', 'Александра', 'Емельяновна', 1, 'emelya123', 'sid2em'),
	(2, 'Петров', 'Иосиф', 'Лаврентьевич', 1, 'petrov', 'petrov');
/*!40000 ALTER TABLE `ucheniki` ENABLE KEYS */;

-- Дамп структуры для таблица school.uroki
CREATE TABLE IF NOT EXISTS `uroki` (
  `id_uroka` int(11) NOT NULL AUTO_INCREMENT,
  `id_raspisaniya` int(11) NOT NULL,
  `id_predmeta` int(11) NOT NULL,
  `id_auditorii` int(11) NOT NULL,
  `poradok` varchar(8) NOT NULL,
  PRIMARY KEY (`id_uroka`),
  KEY `id_raspisaniya` (`id_raspisaniya`),
  KEY `id_predmeta` (`id_predmeta`),
  KEY `id_auditorii` (`id_auditorii`),
  CONSTRAINT `uroki_ibfk_1` FOREIGN KEY (`id_raspisaniya`) REFERENCES `raspisanie` (`id_raspisaniya`) ON DELETE CASCADE,
  CONSTRAINT `uroki_ibfk_2` FOREIGN KEY (`id_predmeta`) REFERENCES `predmety` (`id_predmeta`),
  CONSTRAINT `uroki_ibfk_3` FOREIGN KEY (`id_auditorii`) REFERENCES `auditorii` (`id_auditorii`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COMMENT='Уроки';

-- Дамп данных таблицы school.uroki: ~16 rows (приблизительно)
/*!40000 ALTER TABLE `uroki` DISABLE KEYS */;
INSERT IGNORE INTO `uroki` (`id_uroka`, `id_raspisaniya`, `id_predmeta`, `id_auditorii`, `poradok`) VALUES
	(1, 1, 1, 4, '1-й урок'),
	(8, 1, 2, 4, '2-й урок'),
	(9, 1, 3, 4, '3-й урок'),
	(10, 4, 1, 3, '1-й урок'),
	(11, 4, 3, 2, '2-й урок'),
	(12, 4, 5, 5, '3-й урок'),
	(13, 4, 4, 4, '4-й урок'),
	(14, 7, 1, 1, '1-й урок'),
	(15, 7, 5, 2, '2-й урок'),
	(16, 7, 3, 3, '3-й урок'),
	(17, 7, 2, 4, '4-й урок'),
	(18, 8, 3, 1, '1-й урок'),
	(19, 8, 2, 3, '2-й урок'),
	(20, 8, 1, 5, '3-й урок');
/*!40000 ALTER TABLE `uroki` ENABLE KEYS */;

-- Дамп структуры для таблица school.zanyatiya
CREATE TABLE IF NOT EXISTS `zanyatiya` (
  `id_zanyatiya` int(11) NOT NULL AUTO_INCREMENT,
  `id_uroka` int(11) NOT NULL,
  `date` date NOT NULL,
  `id_prepod` int(11) NOT NULL,
  PRIMARY KEY (`id_zanyatiya`),
  KEY `id_uroka` (`id_uroka`),
  KEY `id_prepod` (`id_prepod`),
  CONSTRAINT `zanyatiya_ibfk_1` FOREIGN KEY (`id_uroka`) REFERENCES `uroki` (`id_uroka`),
  CONSTRAINT `zanyatiya_ibfk_2` FOREIGN KEY (`id_prepod`) REFERENCES `prepod` (`id_prepod`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='Занятия';

-- Дамп данных таблицы school.zanyatiya: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `zanyatiya` DISABLE KEYS */;
INSERT IGNORE INTO `zanyatiya` (`id_zanyatiya`, `id_uroka`, `date`, `id_prepod`) VALUES
	(3, 10, '2020-09-01', 1),
	(4, 20, '2020-09-04', 1),
	(5, 1, '2020-11-13', 1),
	(6, 1, '2020-11-14', 1);
/*!40000 ALTER TABLE `zanyatiya` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
