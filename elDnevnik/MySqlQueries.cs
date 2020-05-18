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
        public string Select_Auditorii = $@"SELECT id_auditorii, nom_auditorii AS 'Номер аудитории' FROM auditorii;";

        public string Select_Auditorii_ComboBox = $@"SELECT nom_auditorii FROM auditorii;";

        public string Select_ID_Auditorii_ComboBox = $@"SELECT id_auditorii FROM auditorii WHERE nom_auditorii = @Value1;";

        public string Select_Predmety = $@"SELECT id_predmeta, naimenovanie AS 'Наименование предмета' FROM predmety;";

        public string Select_Predmety_ComboBox = $@"SELECT naimenovanie FROM predmety;";

        public string Select_ID_Predmety_ComboBox = $@"SELECT id_predmeta FROM predmety WHERE naimenovanie = @Value1;";
        //Select

        //Insert
        public string Insert_Auditorii = $@"INSERT INTO auditorii (nom_auditorii) VALUES (@Value1)";

        public string Insert_Predmety = $@"INSERT INTO predmety (naimenovanie) VALUES (@Value1)";
        //Insert

        //Update
        public string Update_Auditorii = $@"UPDATE auditorii SET nom_auditorii = @Value1 WHERE id_auditorii = @ID;";

        public string Update_Predmety = $@"UPDATE predmety SET naimenovanie = @Value1 WHERE id_predmeta = @ID;";
        //Update

        //Delete
        public string Delete_Auditorii = $@"DELETE FROM auditorii WHERE id_auditorii = @ID;";

        public string Delete_Predmety = $@"DELETE FROM predmety WHERE id_predmeta = @ID;";
        //Delete
    }
}
