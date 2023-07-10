<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend/atsMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZadanieZCT.Frontend.Default" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPLaceHolder1" runat="server">
 
    
<div class="container">
    <h1>Apes Together Strong</h1>


    <div class="row">
      <div class="col-sm-3">
        <div class="card">

          <div class="card-body">
             <h5 class="card-title">Temperature</h5>
            <asp:Label ID="tempLabel" runat="server"><b>°C</b></asp:Label>
            <asp:Label ID="tempSensor" runat="server" Text="Temp sensor placeholder" BorderStyle="None"></asp:Label>
          </div>
        </div>
      </div>
      <div class="col-sm-3">
        <div class="card">

          <div class="card-body">
             <h5 class="card-title">Pressure</h5>
            <asp:Label ID="pressLabel" runat="server"><b>kPa</b></asp:Label>
            <asp:Label ID="pressSensor" runat="server" Text="Pressure_sensor_placeholder"></asp:Label>
          </div>
        </div>
      </div>
      <div class="col-sm-3">
        <div class="card">

          <div class="card-body">
            <h5 class="card-title">Relative humidity</h5>
            <asp:Label ID="humLabel" runat="server"><b>%</b></asp:Label>
            <asp:Label ID="humSensor" runat="server" Text="Humidity sensor placeholder"></asp:Label>
          </div>
        </div>
      </div>
      <div class="col-sm-3">
        <div class="card">

          <div class="card-body">
             <h5 class="card-title">Time</h5>
            <asp:Label ID="dateTimeLabel" runat="server"></asp:Label>
            <asp:Label ID="dateTime" runat="server" Text="datime placeholder"></asp:Label>
          </div>
        </div>
      </div>
    </div>

    <form id="form1" runat="server">
      <div class="form-group">
          <label for="rowSelect">Select count of viewed rows:</label>
          <select class="form-control" id="rowSelect" name="rowSelect">
              <option>5</option>
              <option selected="selected">10</option>
              <option>15</option>
          </select>
      </div>


      <asp:Button runat="server" id="submitBtn" class="btn btn-primary" Text="Select" OnClick="Submit_Click" />

    </form>



    <asp:Table ID="atsTable" runat="server" Width="100%" class="table"> 
        <asp:TableRow>
            <asp:TableCell>Temperature</asp:TableCell>
            <asp:TableCell>Pressure</asp:TableCell>
            <asp:TableCell>Humidity</asp:TableCell>
            <asp:TableCell>Created at</asp:TableCell>
         </asp:TableRow>
    </asp:Table>
</div>


</asp:Content>
