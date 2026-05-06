<%@ Page Title="" Language="C#" MasterPageFile="~/Website/menu.Master" AutoEventWireup="true" CodeBehind="IntemBB.aspx.cs" Inherits="IntemBB.Website.IntemBB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    
    <script src="../Scripts/chosen.jquery.min.js"></script>
    <link href="../Content/bootstrap-chosen.css" rel="stylesheet" />
  
    <style>
        .cssLabel {
            
            font-size: 18px;
            font-weight: bold;
            font-family: Arial;
            color: black;
        }
        .cssLabel1{
             font-size: 18px;
            
            font-family: Arial;
            color: red;

        }
         .cssLabel3{
             font-size: 18px;
            
            font-family: Arial;
            color: white;

        }

        .trcls {
            height: 65px;
        }
        .trcls1{
            width:100px
        }


        .form-control {
            font-size: 25px;
            font-weight: bold;
            font-family: Arial;
            color: black;
        }
        .form-control2 {
            
           
            width:355px;
            height:35px;
            color: black;   
        }
         .form-control1 {
            font-size: 22px;
            font-weight: bold;
            font-family: Arial;
            color: black;
        }
         
        

        input {
            text-transform: uppercase;
        }

        .tablediv {
            position: relative; /*so children div can use 100% height and not go over */
            width: 100%; /* as long as this div's parent is body it will take up whole screen */
            height: 250px; /*make whatever you want */
            padding: 8px; /* will make that spacing you wanted */
        }
            .tablediv .cell {
                width: 48%;
                height: 100%;
                display: inline-block; /*so they display inline */
            }
                .tablediv .cell:first-of-type {
                    width: 48%;
                    height: 100%;
                }
    </style>
     <script type="text/javascript">
         document.onkeydown = function (e) {
             if (e.ctrlKey && (e.keyCode === 85)) {
                 return false;
             }
         }

      
     </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtSolo.ClientID%>").datepicker({ dateFormat: "yy-mm-dd" });
        });


       
    </script>
    <script >
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="text-align:center;">
        <h1 style="font-family:Arial;font-weight:bold;color:#1B2631 ;font-size:50px">In lại tem hóa chất</h1>
         <hr style="border: 1px solid #17202A;" />
    </div>
     <div style="display: flex; align-items: center; justify-content: center; width: 100%; height: 530px">
         <table  style="background-color:#D0ECE7;border:double">
              <tr class="trcls" >
                  <td>
                       <label class="cssLabel">&nbsp;&nbsp;</label>
                  </td>
                  <td colspan="3"> 
                      &emsp; &emsp; &emsp;&emsp; &emsp; &emsp;&emsp; &emsp; &emsp;&emsp; &emsp; &emsp;&emsp; &emsp; &emsp;&emsp; &emsp; &emsp;
                      <asp:Label ID="lblKetnoi1" runat="server"  CssClass="cssLabel1" Text="Chưa kết nối"></asp:Label>

                  </td>
                
                 

              </tr>
             <tr class="trcls">

                 <td>
                    <label class="cssLabel">&nbsp;&nbsp;Chọn máy:&nbsp;&nbsp;</label>
                </td>
                 <td  colspan="4" >
                     &emsp; &emsp; &emsp;<asp:RadioButton ID="rdMay1" Text="--Máy -1" Font-Size="20px" AutoPostBack="true" OnCheckedChanged="rdMay1_CheckedChanged"  GroupName="nam" runat="server" />
                      &emsp; &emsp; &emsp;<asp:RadioButton ID="rdMay2" Text="--Máy -9" Font-Size="20px" AutoPostBack="true" OnCheckedChanged="rdMay2_CheckedChanged" GroupName="nam" runat="server" />
                      &emsp; &emsp; &emsp;<asp:RadioButton ID="rdMay02" Text="--Máy -1 mới" Font-Size="20px" AutoPostBack="true" OnCheckedChanged="rdMay02_CheckedChanged" GroupName="nam" runat="server" />
                      &emsp; &emsp; &emsp;<asp:RadioButton ID="rdMay04" Text="--Máy -9 mới" Font-Size="20px" AutoPostBack="true" OnCheckedChanged="rdMay04_CheckedChanged" GroupName="nam" runat="server" />
                 </td>
             
                 
             </tr>
             <tr class="trcls">
                 <td>
                     <label class="cssLabel">&nbsp;&nbsp;Số Lô:&nbsp;&nbsp;</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtSolo" runat="server" autocomplete="off" CssClass="form-control" OnTextChanged="txtSolo_TextChanged" AutoPostBack="true" placeholder="#Chọn số lô"></asp:TextBox>
                 </td>
                 <td>
                      <label class="cssLabel">&nbsp;&nbsp;Thời gian sản xuất:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtTGSX" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                 </td>
             </tr>
             <tr class="trcls">
                 <td>
                     <asp:Label  class="cssLabel" ID="lblTB" runat="server" >&nbsp;&nbsp;Chất phối hợp:&nbsp;&nbsp;</asp:Label>
                     
                 </td>
                 <td>
                     <asp:DropDownList ID="drChonmakeo" AutoPostBack="true" OnSelectedIndexChanged="drChonmakeo_SelectedIndexChanged"  CssClass="form-control2" runat="server"></asp:DropDownList>
             
                 </td>
                 <td>
                     <label class="cssLabel">&nbsp;&nbsp;Ngày hiệu lực:&nbsp;&nbsp;</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtNHL" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                 </td>
             </tr>
             <tr class="trcls">
                 <%--<td>
                      <label class="cssLabel">Tìm kiếm chất:&nbsp;&nbsp;</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtTimkiem" runat="server" CssClass="form-control"></asp:TextBox>
                 </td>--%>
                 <td>
                     <label class="cssLabel">&nbsp;&nbsp;Người thao tác:&nbsp;&nbsp;</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtNguoiTT" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                 </td>
                 <td>
                     <label class="cssLabel">&nbsp;&nbsp;Nhập số bọc hỏng:&nbsp;&nbsp;</label>
                 </td>

                 <td colspan ="2">
                      <div style="display: flex; gap: 10px;"> <!-- 'gap' thêm khoảng cách giữa các textbox -->
                            <asp:TextBox ID="txtSoMeSX" runat="server" CssClass="form-control" style="width: 100px;" ></asp:TextBox>
                          <Label style="font-size:24px">/</Label>
                             <asp:TextBox ID="txtPlanNum" runat="server" Text="0" CssClass="form-control" ReadOnly="true" style="width: 100px;"></asp:TextBox>
                       </div>
                    
                 </td>
                 <td>
                      
                    
                 </td>

             </tr>

            
             <tr class="trcls">
                  <td>
                      <label class="cssLabel">&nbsp;&nbsp;Mở cho máy :&nbsp;&nbsp;</label>
                 </td>
                 <td >
                     <asp:DropDownList ID="drMaybb" runat="server" CssClass="form-control1">
                         <asp:ListItem>BB 01</asp:ListItem>
                         <asp:ListItem>BB 02</asp:ListItem>
                         <asp:ListItem>BB 03</asp:ListItem>
                        <asp:ListItem>BB 04</asp:ListItem>
                         <asp:ListItem>BB 05</asp:ListItem>
                        <asp:ListItem>BB 06</asp:ListItem>
                         <asp:ListItem>BB 07</asp:ListItem>
                         <asp:ListItem>BB 08</asp:ListItem>

                      
         
                     </asp:DropDownList>

                 </td>
                 <td>
                      <label class="cssLabel">&nbsp;&nbsp;  Máy in:&nbsp;&nbsp;</label>
                 </td>
                 <td colspan="2">
                     <asp:DropDownList ID="drMayin" runat="server" CssClass="form-control1">
                         <asp:ListItem>Hoa chat 198.1.8.111</asp:ListItem>
                         <asp:ListItem>Hoa Chat New 8.112</asp:ListItem>
                        <%-- <asp:ListItem>tổng đài 10.198 on 198.1.10.198</asp:ListItem>--%>
                         
                     </asp:DropDownList>

                 </td>
                

   

             </tr>
              <tr class="trcls">
                 

                 <td>

                     <label class="cssLabel">&nbsp;&nbsp;Mã mes:&nbsp;&nbsp;</label>
                 </td>
                 <td>
                      
                     <asp:TextBox ID="txtPlanid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                     <asp:Label ID="lblThoiGianKT" class="cssLabel3" runat="server"></asp:Label>
                   
                     
                    
       
                    
                 </td >
                   <td colspan="2" >
                     <asp:Button ID="btnIntem" OnClientClick="showLoading();" OnClick="btnIntem_Click"  Style="font-weight: bold; font-size: 16px;margin-left:50px; font-family: Arial;width:150px;" CssClass="btn btn-success" runat="server" Text="InTem" />
                 </td>
             </tr>

         </table>
         <script>
             $('#<%=drChonmakeo.ClientID%>').chosen();
         </script>
     </div> 
     <table id="tblMessages" style="display:none;position:absolute;top:350px;left:0;width:100%";height:100%; backgroung-color:Yellow;>
                <tr>
                    <td>
                        <table id="tblMessages1" style="background-color:red; margin-left:auto;margin-right:auto;box-shadow:0 0 10px 20px gray; border-radius:5px;width:500px;height:200px;">
                            <tr>
                                <td style="height: 35px; padding-left:10px;color:black;">
                                    Thông báo
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color:white;vertical-align: top; padding:10px;">

                                    <asp:Label ID="lblThongbao" runat="server" Style="font-family:Arial;font-weight:bold; font-size:16px;color:black" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:1px; background-color:white; text-align:right; padding:5px;border-radius: 0 0 5px 5px;">
                                    <button type="button" class="btn btn-danger" onclick="closeMessage();">Đóng</button>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
            </table>
</asp:Content>
