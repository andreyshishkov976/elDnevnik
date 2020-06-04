using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elDnevnik
{
    public class MySqlQueries
    {
        //Exists
        public string Exists_Uroki = $@"SELECT EXISTS(SELECT * FROM uroki WHERE id_raspisaniya = @Value1 AND poradok = @Value2);";

        public string Exists_Raspisanie = $@"SELECT EXISTS(SELECT * FROM raspisanie WHERE id_klassa = @Value1 AND den_nedeli = @Value2);";

        public string Exists_Auditorii = $@"SELECT EXISTS(SELECT * FROM auditorii WHERE nom_auditorii = @Value1);";
        
        public string Exists_Klassy = $@"SELECT EXISTS(SELECT * FROM klassy WHERE nom_klassa = @Value1 AND parallel = @Value2);";

        public string Exists_Predmety = $@"SELECT EXISTS(SELECT * FROM predmety WHERE naimenovanie = @Value1);";

        public string Exists_Prepod = $@"SELECT EXISTS(SELECT * FROM prepod WHERE login = @Value1 AND parol = @Value2);";

        public string Exists_Ucheniki = $@"SELECT EXISTS(SELECT * FROM ucheniki WHERE login = @Value1 AND parol = @Value2);";

        public string Exists_Zanyatiya_Today = $@"SELECT EXISTS(SELECT CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Класс', uroki.poradok AS 'Урок п/п', predmety.naimenovanie AS 'Предмет', auditorii.nom_auditorii AS 'Аудитория'
FROM uroki INNER JOIN raspisanie ON uroki.id_raspisaniya = raspisanie.id_raspisaniya
INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
INNER JOIN predmety ON uroki.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
INNER JOIN prepod ON predmety.id_predmeta = prepod.id_predmeta
WHERE prepod.id_prepod = @ID AND raspisanie.den_nedeli = @Value1
ORDER BY uroki.poradok ASC);";

        public string Exists_Zanyatiya = $@"SELECT EXISTS(SELECT * FROM zanyatiya WHERE id_uroka = @Value1 AND date = @Value2 AND id_prepod = @Value3);";
        //Exists

        //Select
        public string Select_Last_ID = $@"SELECT LAST_INSERT_ID();";

        public string Select_Auditorii = $@"SELECT id_auditorii, nom_auditorii AS 'Номер аудитории' FROM auditorii;";

        public string Select_Auditorii_ComboBox = $@"SELECT nom_auditorii FROM auditorii;";

        public string Select_ID_Auditorii_ComboBox = $@"SELECT id_auditorii FROM auditorii WHERE nom_auditorii = @Value1;";

        public string Select_Predmety = $@"SELECT id_predmeta, naimenovanie AS 'Наименование предмета' FROM predmety;";

        public string Select_Predmety_ComboBox = $@"SELECT naimenovanie FROM predmety;";

        public string Select_Predmet_Prepoda = $@"SELECT naimenovanie FROM predmety INNER JOIN prepod ON predmety.id_predmeta = prepod.id_predmeta
WHERE prepod.id_prepod = @ID;";

        public string Select_ID_Predmety_ComboBox = $@"SELECT id_predmeta FROM predmety WHERE naimenovanie = @Value1;";

        public string Select_Fakultativy = $@"SELECT id_fakultativa, predmety.naimenovanie AS 'Наименование предмета', date_provedeniya AS 'Дата проведения',
auditorii.nom_auditorii AS 'Номер аудитории', CONCAT(familiya,' ',imya,' ',otchestvo) AS 'Ф.И.О. предователя',
TIME_FORMAT(time_n, '%H:%i') AS 'Время начала', TIME_FORMAT(time_k, '%H:%i') AS 'Время окончания'
FROM fakultativy INNER JOIN predmety ON fakultativy.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON fakultativy.id_auditorii = auditorii.id_auditorii
INNER JOIN prepod ON fakultativy.id_prepod = prepod.id_prepod;";

        public string Select_Fakultativy_Prepoda = $@"SELECT id_fakultativa, date_provedeniya AS 'Дата проведения',
auditorii.nom_auditorii AS 'Номер аудитории', 
TIME_FORMAT(time_n, '%H:%i') AS 'Время начала', TIME_FORMAT(time_k, '%H:%i') AS 'Время окончания'
FROM fakultativy INNER JOIN predmety ON fakultativy.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON fakultativy.id_auditorii = auditorii.id_auditorii
INNER JOIN prepod ON fakultativy.id_prepod = prepod.id_prepod
WHERE prepod.id_prepod = @ID;";

        public string Select_Fakultativy_Prepoda_Filter = $@"SELECT id_fakultativa, date_provedeniya AS 'Дата проведения',
auditorii.nom_auditorii AS 'Номер аудитории', 
TIME_FORMAT(time_n, '%H:%i') AS 'Время начала', TIME_FORMAT(time_k, '%H:%i') AS 'Время окончания'
FROM fakultativy INNER JOIN predmety ON fakultativy.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON fakultativy.id_auditorii = auditorii.id_auditorii
INNER JOIN prepod ON fakultativy.id_prepod = prepod.id_prepod
WHERE prepod.id_prepod = @ID AND (date_provedeniya LIKE @Value1 AND auditorii.nom_auditorii LIKE @Value1 OR TIME_FORMAT(time_n, '%H:%i') LIKE @Value1 OR TIME_FORMAT(time_k, '%H:%i') LIKE @Value1);";

        public string Select_Klassy = $@"SELECT id_klassa, CONCAT(nom_klassa,' ',parallel) AS 'Класс', kolvo_uch AS 'Количество учащихся' FROM klassy;";

        public string Select_Klassy_ComboBox = $@"SELECT CONCAT(nom_klassa,' ',parallel) FROM klassy;";

        public string Select_ID_Klassy_ComboBox = $@"SELECT id_klassa FROM klassy WHERE CONCAT(nom_klassa,' ',parallel) = @Value1;";

        public string Select_Prepod = $@"SELECT id_prepod, CONCAT(familiya,' ',imya,' ',otchestvo) AS 'Ф.И.О. предователя',
predmety.naimenovanie AS 'Преподаваемый предмет', login AS 'Логин', parol AS 'Пароль'
FROM prepod INNER JOIN predmety ON prepod.id_predmeta = predmety.id_predmeta;";

        public string Select_Prepod_ComboBox = $@"SELECT CONCAT(familiya,' ',imya,' ',otchestvo) FROM prepod;";

        public string Select_ID_Prepod_ComboBox = $@"SELECT id_prepod FROM prepod WHERE CONCAT(familiya,' ',imya,' ',otchestvo) = @Value1;";

        public string Select_ID_Prepod = $@"SELECT id_prepod FROM prepod WHERE login = @Value1 AND parol = @Value2;";

        public string Select_FIO_Prepod = $@"SELECT CONCAT(familiya, ' ', imya, ' ', otchestvo) FROM prepod WHERE id_prepod = @ID;";

        public string Select_Ucheniki = $@"SELECT id_uchenika, CONCAT(familiya,' ',imya,' ',otchestvo) AS 'Ф.И.О. ученика',
CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Текущий класс', login AS 'Логин', parol AS 'Пароль'
FROM ucheniki INNER JOIN klassy ON ucheniki.id_klassa = klassy.id_klassa;";

        public string Select_Raspisanie = $@"SELECT raspisanie.id_raspisaniya, CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Номер класса',
raspisanie.den_nedeli = 5 AS 'День недели', COUNT(uroki.id_uroka) AS 'Количество уроков'
FROM raspisanie INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
INNER JOIN uroki ON raspisanie.id_raspisaniya = uroki.id_raspisaniya
GROUP BY raspisanie.id_raspisaniya
ORDER BY raspisanie.den_nedeli;";

        public string Select_Uroki_Raspisaniya = $@"SELECT id_uroka, predmety.naimenovanie AS 'Наименование предмета', auditorii.nom_auditorii AS 'Номер аудитории', poradok AS 'Урок по порядку'
FROM uroki INNER JOIN predmety ON uroki.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
WHERE id_raspisaniya = @ID;";

        public string Select_Uroki_Uchenika = $@"SELECT uroki.id_uroka, uroki.poradok AS 'Урок п/п', predmety.naimenovanie AS 'Предмет', auditorii.nom_auditorii AS 'Аудитория'
FROM uroki INNER JOIN raspisanie ON uroki.id_raspisaniya = raspisanie.id_raspisaniya
INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
INNER JOIN predmety ON uroki.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
WHERE klassy.id_klassa = @Value1 AND raspisanie.den_nedeli = @Value2;";

        public string Select_Uroki_Prepoda = $@"SELECT uroki.id_uroka, CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Класс', uroki.poradok AS 'Урок п/п', predmety.naimenovanie AS 'Предмет', auditorii.nom_auditorii AS 'Аудитория'
FROM uroki INNER JOIN raspisanie ON uroki.id_raspisaniya = raspisanie.id_raspisaniya
INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
INNER JOIN predmety ON uroki.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
INNER JOIN prepod ON predmety.id_predmeta = prepod.id_predmeta
WHERE prepod.id_prepod = @ID AND raspisanie.den_nedeli = @Value1
ORDER BY uroki.poradok ASC;";

        public string Select_Zanyatiya = $@"SELECT zanyatiya.id_zanyatiya, zanyatiya.date AS 'Дата', 
raspisanie.den_nedeli AS 'День недели', 
uroki.poradok AS 'Урок п/п', auditorii.nom_auditorii AS 'Номер аудитории', 
CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Номер класса'
FROM zanyatiya INNER JOIN uroki ON zanyatiya.id_uroka = uroki.id_uroka
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
INNER JOIN raspisanie ON uroki.id_raspisaniya = raspisanie.id_raspisaniya
INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
WHERE zanyatiya.id_prepod = @ID;";

        public string Select_Zanyatiya_Filter = $@"SELECT zanyatiya.id_zanyatiya, zanyatiya.date AS 'Дата', 
raspisanie.den_nedeli AS 'День недели', 
uroki.poradok AS 'Урок п/п', auditorii.nom_auditorii AS 'Номер аудитории', 
CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Номер класса'
FROM zanyatiya INNER JOIN uroki ON zanyatiya.id_uroka = uroki.id_uroka
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
INNER JOIN raspisanie ON uroki.id_raspisaniya = raspisanie.id_raspisaniya
INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa
WHERE zanyatiya.id_prepod = @ID AND (zanyatiya.date LIKE @Value1 OR raspisanie.den_nedeli LIKE @Value1 OR uroki.poradok LIKE @Value1 OR CONCAT(klassy.nom_klassa, ' ', klassy.parallel) LIKE @Value1);";

        public string Select_ID_Zanyatiya = $@"SELECT id_zanyatiya FROM zanyatiya WHERE id_uroka = @Value1 AND date = @Value2 AND id_prepod = @Value3;";

        public string Select_Homework = $@"SELECT zadanie FROM homework WHERE id_homework = @ID;";

        public string Select_ID_Homework = $@"SELECT homework.id_homework FROM homework INNER JOIN zanyatiya ON homework.id_zanyatiya = zanyatiya.id_zanyatiya
WHERE zanyatiya.id_zanyatiya = @ID;";

        public string Select_ID_Ucheniki_Klassa = $@"SELECT ucheniki.id_uchenika FROM ucheniki
INNER JOIN klassy ON ucheniki.id_klassa = klassy.id_klassa
WHERE CONCAT(klassy.nom_klassa, ' ', klassy.parallel) = @Value1;";

        public string Select_Otmetki_Zanyatiya = $@"SELECT otmetki.id_otmetki, CONCAT(ucheniki.familiya, ' ', ucheniki.imya, ' ', ucheniki.otchestvo) AS 'Ф.И.О. ученика', otmetki.znachenie AS 'Отметка'
FROM otmetki INNER JOIN ucheniki ON otmetki.id_uchenika = ucheniki.id_uchenika
WHERE otmetki.id_zanyatiya = @ID;";

        public string Select_Jurnal_Klassa = $@"SELECT 
        CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo) AS 'Ф.И.О. ученика',
  		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 01 THEN otmetki.znachenie ELSE null END AS '01',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 02 THEN otmetki.znachenie ELSE null END AS '02',
        CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 03 THEN otmetki.znachenie ELSE null END AS '03',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 04 THEN otmetki.znachenie ELSE null END AS '04',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 05 THEN otmetki.znachenie ELSE null END AS '05',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 06 THEN otmetki.znachenie ELSE NULL END AS '06',
        CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 07 THEN otmetki.znachenie ELSE null END AS '07',
        CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 08 THEN otmetki.znachenie ELSE NULL END AS '08',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 09 THEN otmetki.znachenie ELSE null END AS '09',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 10 THEN otmetki.znachenie ELSE null END AS '10',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 11 THEN otmetki.znachenie ELSE null END AS '11',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 12 THEN otmetki.znachenie ELSE null END AS '12',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 13 THEN otmetki.znachenie ELSE null END AS '13',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 14 THEN otmetki.znachenie ELSE null END AS '14',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 15 THEN otmetki.znachenie ELSE null END AS '15',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 16 THEN otmetki.znachenie ELSE null END AS '16',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 17 THEN otmetki.znachenie ELSE null END AS '17',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 18 THEN otmetki.znachenie ELSE null END AS '18',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 19 THEN otmetki.znachenie ELSE null END AS '19',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 20 THEN otmetki.znachenie ELSE null END AS '20',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 21 THEN otmetki.znachenie ELSE null END AS '21',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 22 THEN otmetki.znachenie ELSE null END AS '22',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 23 THEN otmetki.znachenie ELSE null END AS '23',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 24 THEN otmetki.znachenie ELSE null END AS '24',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 25 THEN otmetki.znachenie ELSE null END AS '25',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 26 THEN otmetki.znachenie ELSE null END AS '26',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 27 THEN otmetki.znachenie ELSE null END AS '27',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 28 THEN otmetki.znachenie ELSE null END AS '28',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 29 THEN otmetki.znachenie ELSE NULL END AS '29',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 30 THEN otmetki.znachenie ELSE null END AS '30',
		CASE DATE_FORMAT(zanyatiya.date, '%d') WHEN 31 THEN otmetki.znachenie ELSE null END AS '31'
FROM otmetki INNER JOIN ucheniki ON otmetki.id_uchenika = ucheniki.id_uchenika
INNER JOIN zanyatiya ON otmetki.id_zanyatiya = zanyatiya.id_zanyatiya
INNER JOIN prepod ON zanyatiya.id_prepod = prepod.id_prepod
WHERE DATE_FORMAT(zanyatiya.date, '%Y-%m') = @Value1 AND prepod.id_prepod = @Value2 AND ucheniki.id_klassa = @Value3
GROUP BY CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo)
ORDER BY CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo);";

        public string Select_Uspevaemost_Klassa = $@"SELECT CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo) AS 'Ф.И.О. ученика', AVG(otmetki.znachenie) AS 'Средний балл'
FROM otmetki INNER JOIN ucheniki ON otmetki.id_uchenika = ucheniki.id_uchenika
INNER JOIN zanyatiya ON otmetki.id_zanyatiya = zanyatiya.id_zanyatiya
INNER JOIN prepod ON zanyatiya.id_prepod = prepod.id_prepod
WHERE DATE_FORMAT(zanyatiya.date, '%Y-%m') = @Value1 AND ucheniki.id_klassa = @Value2
GROUP BY CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo)
ORDER BY CONCAT(ucheniki.familiya,' ',ucheniki.imya,' ',ucheniki.otchestvo);";

        public string Select_SrBal_Klassa = $@"SELECT AVG(otmetki.znachenie)
FROM otmetki INNER JOIN ucheniki ON otmetki.id_uchenika = ucheniki.id_uchenika
INNER JOIN zanyatiya ON otmetki.id_zanyatiya = zanyatiya.id_zanyatiya
WHERE DATE_FORMAT(zanyatiya.date, '%Y-%m') = @Value1 AND ucheniki.id_klassa = @Value2;";
        //Select

        //Insert
        public string Insert_Auditorii = $@"INSERT INTO auditorii (nom_auditorii) VALUES (@Value1)";

        public string Insert_Predmety = $@"INSERT INTO predmety (naimenovanie) VALUES (@Value1)";

        public string Insert_Fakultativy = $@"INSERT INTO fakultativy (id_predmeta, id_prepod, date_provedeniya, id_auditorii, time_n, time_k) VALUES (@Value1, @Value2, @Value3, @value4, @Value5, @Value6);";

        public string Insert_Klassy = $@"INSERT INTO klassy (nom_klassa, parallel, kolvo_uch) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Prepod = $@"INSERT INTO prepod (familiya, imya, otchestvo, id_predmeta, login, parol) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6);";

        public string Insert_Ucheniki = $@"INSERT INTO ucheniki (familiya, imya, otchestvo, id_klassa, login, parol) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6);";

        public string Insert_Raspisanie = $@"INSERT INTO raspisanie (id_klassa, den_nedeli) VALUES (@Value1, @Value2);";

        public string Insert_Uroki = $@"INSERT INTO uroki (id_raspisaniya, id_predmeta, id_auditorii, poradok) VALUES (@Value1, @Value2, @Value3, @Value4);";

        public string Insert_Zanyatiya = $@"INSERT INTO zanyatiya (id_uroka, date, id_prepod) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Otmetki = $@"INSERT INTO otmetki (id_uchenika, id_zanyatiya, znachenie) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Homework = $@"INSERT INTO homework (id_zanyatiya, zadanie, date) VALUES (@Value1, '', @Value2);";
        //Insert

        //Update
        public string Update_Auditorii = $@"UPDATE auditorii SET nom_auditorii = @Value1 WHERE id_auditorii = @ID;";

        public string Update_Predmety = $@"UPDATE predmety SET naimenovanie = @Value1 WHERE id_predmeta = @ID;";

        public string Update_Fakultativy = $@"UPDATE fakultativy SET id_predmeta = @Value1, id_prepod = @Value2, date_provedeniya = @Value3, id_auditorii = @Value4, time_n = @Value5, time_k = @Value6 WHERE id_fakultativa = @ID;";

        public string Update_Klassy = $@"UPDATE klassy SET nom_klassa = @Value1, parallel = @Value2, kolvo_uch = @Value3 WHERE id_klassa = @ID;";

        public string Update_Prepod = $@"UPDATE prepod SET familiya = @Value1, imya = @Value2, otchestvo = @Value3, id_predmeta = @Value4, login = @Value5, parol = @Value6 WHERE id_prepod = @ID;";

        public string Update_Ucheniki = $@"UPDATE ucheniki SET familiya = @Value1, imya = @Value2, otchestvo = @Value3, id_klassa = @Value4, login = @Value5, parol = @Value6 WHERE id_uchenika = @ID;";

        public string Update_Raspisanie = $@"UPDATE raspisanie SET id_klassa = @Value1, den_nedeli = @Value2 WHERE id_raspisaniya = @ID;";

        public string Update_Uroki = $@"UPDATE uroki SET id_raspisaniya = @Value1, id_predmeta = @Value2, id_auditorii = @Value3, poradok = @Value4 WHERE id_uroka = @ID;";

        public string Update_Otmetki = $@"UPDATE otmetki SET znachenie = @Value1 WHERE id_otmetki = @ID;";

        public string Update_Homework = $@"UPDATE homework SET zadanie = @Value1 WHERE id_homework = @ID;";
        //Update

        //Delete
        public string Delete_Auditorii = $@"DELETE FROM auditorii WHERE id_auditorii = @ID;";

        public string Delete_Predmety = $@"DELETE FROM predmety WHERE id_predmeta = @ID;";

        public string Delete_Fakultativy = $@"DELETE FROM fakultativy WHERE id_fakultativa = @ID;";

        public string Delete_Klassy = $@"DELETE FROM klassy WHERE id_klassa = @ID;";

        public string Delete_Prepod = $@"DELETE FROM prepod WHERE id_prepod = @ID;";

        public string Delete_Ucheniki = $@"DELETE FROM uchenika WHERE id_uchenika = @ID;";

        public string Delete_Raspisanie = $@"DELETE FROM raspisanie WHERE id_raspisaniya = @ID;";
        
        public string Delete_Uroki = $@"DELETE FROM uroki WHERE id_uroka = @ID;";

        public string Delete_Zanyatiya = $@"DELETE FROM zanyatiya WHERE id_zanyatiya = @ID;";
        //Delete
    }
}
