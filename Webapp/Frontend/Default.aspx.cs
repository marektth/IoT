using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ZadanieZCT.Backend;

namespace ZadanieZCT.Frontend
{
    public partial class Default : System.Web.UI.Page
    {
        private EnvSensorAWS latestData;
        private List<EnvSensorSQL> env_values = new List<EnvSensorSQL>();
        private int numberOfRows = 10;



        protected void UpdateTableFromDB()
        {
            numberOfRows = Convert.ToInt32(Request.Form["rowSelect"]);

            env_values = SQLController.GetFromDB(numberOfRows);
            try
            {
                TableRow row;
                TableCell tempCell;
                TableCell humCell;
                TableCell pressCell;
                TableCell createdAtCell;

                foreach (EnvSensorSQL latest in env_values)
                {
                    row = new TableRow();
                    tempCell = new TableCell();
                    humCell = new TableCell();
                    pressCell = new TableCell();
                    createdAtCell = new TableCell();

                    tempCell.Text = latest.GetTemperature().ToString();
                    humCell.Text = latest.GetHumidity().ToString();
                    pressCell.Text = latest.GetPressure().ToString();
                    createdAtCell.Text = latest.GetCreated_at().ToString();

                    row.Cells.Add(tempCell);
                    row.Cells.Add(humCell);
                    row.Cells.Add(pressCell);
                    row.Cells.Add(createdAtCell);

                    atsTable.Rows.Add(row);
                }
            }
            catch
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            latestData = AWSController.GetLatestDataFromAWS();
            tempSensor.Text = latestData.GetTemperature().ToString();
            humSensor.Text = latestData.GetHumidity().ToString();
            pressSensor.Text = latestData.GetPressure().ToString();
            dateTime.Text = latestData.GetCreated_at().ToString();
            SQLController.UpdateDB(latestData);

            UpdateTableFromDB();
        }


        protected void Submit_Click(object sender, EventArgs e)
        {

        }
    }
}