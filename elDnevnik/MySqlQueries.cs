using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elDnevnik
{
    public class MySqlQueries
    {
        //Select
        public string Select_Last_ID = $@"SELECT LAST_INSERT_ID();";

        public string Select_Auditorii = $@"SELECT id_auditorii, nom_auditorii AS 'Номер аудитории' FROM auditorii;";

        public string Select_Auditorii_ComboBox = $@"SELECT nom_auditorii FROM auditorii;";

        public string Select_ID_Auditorii_ComboBox = $@"SELECT id_auditorii FROM auditorii WHERE nom_auditorii = @Value1;";

        public string Select_Predmety = $@"SELECT id_predmeta, naimenovanie AS 'Наименование предмета' FROM predmety;";

        public string Select_Predmety_ComboBox = $@"SELECT naimenovanie FROM predmety;";

        public string Select_ID_Predmety_ComboBox = $@"SELECT id_predmeta FROM predmety WHERE naimenovanie = @Value1;";

        public string Select_Fakultativy = $@"SELECT id_fakultativa, predmety.naimenovanie AS 'Наименование предмета', date_provedeniya AS 'Дата проведения',
auditorii.nom_auditorii AS 'Номер аудитории', 
TIME_FORMAT(time_n, '%H:%i') AS 'Время начала', TIME_FORMAT(time_k, '%H:%i') AS 'Время окончания'
FROM fakultativy INNER JOIN predmety ON fakultativy.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON fakultativy.id_auditorii = auditorii.id_auditorii;";

        public string Select_Klassy = $@"SELECT id_klassa, CONCAT(nom_klassa,' ',parallel) AS 'Класс', kolvo_uch AS 'Количество учащихся' FROM klassy;";

        public string Select_Klassy_ComboBox = $@"SELECT CONCAT(nom_klassa,' ',parallel) FROM klassy;";

        public string Select_ID_Klassy_ComboBox = $@"SELECT id_klassa FROM klassy WHERE CONCAT(nom_klassa,' ',parallel) = @Value1;";

        public string Select_Prepod = $@"SELECT id_prepod, CONCAT(familiya,' ',imya,' ',otchestvo) AS 'Ф.И.О. предователя',
predmety.naimenovanie AS 'Преподаваемый предмет', login AS 'Логин', parol AS 'Пароль'
FROM prepod INNER JOIN predmety ON prepod.id_predmeta = predmety.id_predmeta;";

        public string Select_Prepod_ComboBox = $@"SELECT CONCAT(familiya,' ',imya,' ',otchestvo) FROM prepod;";

        public string Select_ID_Prepod_ComboBox = $@"SELECT id_prepod FROM prepod WHERE CONCAT(familiya,' ',imya,' ',otchestvo) = @Value1;";

        public string Select_Ucheniki = $@"SELECT id_uchenika, CONCAT(familiya,' ',imya,' ',otchestvo) AS 'Ф.И.О. предователя',
CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Текущий класс', login AS 'Логин', parol AS 'Пароль'
FROM ucheniki INNER JOIN klassy ON ucheniki.id_klassa = klassy.id_klassa;";

        public string Select_Raspisanie = $@"SELECT id_raspisaniya, CONCAT(klassy.nom_klassa, ' ', klassy.parallel) AS 'Номер класса', den_nedeli AS 'День недели'
FROM raspisanie INNER JOIN klassy ON raspisanie.id_klassa = klassy.id_klassa;";

        public string Select_Uroki = $@"SELECT id_uroka, predmety.naimenovanie AS 'Наименование предмета', auditorii.nom_auditorii AS 'Номер аудитории', poradok AS 'Урок по порядку'
FROM uroki INNER JOIN predmety ON uroki.id_predmeta = predmety.id_predmeta
INNER JOIN auditorii ON uroki.id_auditorii = auditorii.id_auditorii
WHERE id_raspisaniya = @ID;";
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
        //Delete
    }
}
