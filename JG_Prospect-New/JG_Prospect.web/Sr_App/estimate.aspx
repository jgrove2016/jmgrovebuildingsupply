<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="estimate.aspx.cs" Inherits="JG_Prospect.Sr_App.estimate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
<h1><b>Customer</b></h1>
<!-- Tabs starts --><div class="form_panel_custom">

         
      <div id="accordion">
	<h3><a href="#">Details</a></h3>
	<div>
		<div class="form_panel_custom">
        <ul>
              <li>
                <table border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><label>Customer Name<span>*</span></label>
                        <asp:TextBox ID="txtCust_name" runat="server" ValidationGroup="createEst" onkeyup="javascript:Alpha(this)" TabIndex="1"></asp:TextBox>
                        <%--<input name="input" type="text" />--%><br />
                     <label>  </label> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Please enter Customer Name."   ValidationGroup="estimatebutton"
                            ControlToValidate="txtCust_name" ForeColor="Red"></asp:RequiredFieldValidator>
                        
                    </td>
                  </tr>
                  <tr>
                    <td><label>Customer Street<span>*</span></label>
                        <asp:TextBox ID="txtCust_strt" runat="server" TabIndex="3"></asp:TextBox>
                          <label>
                       
                        </label>     
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                             ValidationGroup="estimatebutton"
                            ErrorMessage="Please Enter Customer Street." 
                            ControlToValidate="txtCust_strt" ForeColor="Red"></asp:RequiredFieldValidator>
                        <%--<input name="input" type="text" />--%>
                      </td>
                  </tr>
                  <tr>
                    <td><label>Customer State, <br />
                      City, Zip Code</label>
                        <asp:DropDownList ID="ddlState" runat="server" Width="60px" TabIndex="5">
                            <asp:ListItem>California</asp:ListItem>
                        </asp:DropDownList>
                        <%--<select name="select" style="width:60px;"><option>Select</option></select>--%>

                        <asp:DropDownList ID="ddlCity" runat="server" Width="60px" TabIndex="6">
                            <asp:ListItem>Los Angeles</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select name="select" style="width:60px;"><option>Select</option></select>--%>

                        <asp:DropDownList ID="ddlZipcode" runat="server" Width="60px" TabIndex="7"
                            AutoPostBack="True" onselectedindexchanged="ddlZipcode_SelectedIndexChanged">
                            <asp:ListItem>90001</asp:ListItem>
                            <asp:ListItem>90002</asp:ListItem>
                        </asp:DropDownList>
                        <%--<select name="select4" style="width:60px;"><option>Select</option></select>--%>
                      
                      </td>
                  </tr>
                  <tr>
                    <td><label>Job Location<span>*</span></label>
                        <asp:TextBox ID="txtJob_loc" runat="server" TabIndex="9"></asp:TextBox>
                        <%--<input type="text" name="textfield2" id="textfield2" />--%>
                  <label></label>      
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please enter Job Location." 
                            ControlToValidate="txtJob_loc" ForeColor="Red"></asp:RequiredFieldValidator>
                     
                      </td>
                  </tr>
                  <tr>
                    <td><label>Est. Date Schedule<span>*</span></label>
                        <asp:TextBox ID="txtEsti_date" runat="server" TabIndex="11"
                            ontextchanged="txtEsti_date_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEsti_date">
                        </asp:CalendarExtender>

                        <%-- <div id="time1"><div>Visit Time:<br /><input name="s2Time1" value="" /><br /></div></div>--%>
          
                    <label></label>    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please Select Est. Date Schedule." 
                            ControlToValidate="txtEsti_date" ForeColor="Red"></asp:RequiredFieldValidator>
                     
                      </td>
                  </tr>
                  
               </table>
              </li>
              <li >
                <table border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><label>Date Ist Contact<span>*</span></label>
                        <asp:TextBox ID="txtDate_frstcontc" runat="server" TabIndex="2"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate_frstcontc">
                        </asp:CalendarExtender>
                     
                   <label>
                       
                        </label>     
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                             ValidationGroup="estimatebutton"
                            ErrorMessage="Please Select Date first Contact." 
                            ControlToValidate="txtDate_frstcontc" ForeColor="Red"></asp:RequiredFieldValidator>
                        
                     
                      </td>
                  </tr>
                  <tr>
                    <td><label>Primary Phone No.<span>*</span></label>
                        <asp:TextBox ID="txtPri_ph" runat="server" TabIndex="4" onkeyup="javascript:Numeric(this)"></asp:TextBox>
                           <label>
                       
                        </label>     
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                             ValidationGroup="estimatebutton"
                            ErrorMessage="Please Enter Primary Phone No." 
                            ControlToValidate="txtPri_ph" ForeColor="Red"></asp:RequiredFieldValidator>
                        <%--<input name="input2" type="text" />--%>
                    </td>
                  </tr>
                  <tr>
                    <td><label>Secondary Phone No.</label>
                        <asp:TextBox ID="txtSec_ph" runat="server" TabIndex="8" onkeyup="javascript:Numeric(this)"></asp:TextBox>
                        <%--<input name="input5" type="text" />--%>
                    </td>
                  </tr>
                  <tr>
                    <td><label>E-mail<span>*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="10"></asp:TextBox>
                        <%--<input name="input6" type="text" />--%>
                  <label></label>      
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"  ValidationGroup="estimatebutton"
                            ErrorMessage="Please enter Email address." 
                            ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                     <br />
                     <label></label>
                       
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="estimatebutton"
                            ControlToValidate="txtEmail" Display="Dynamic"
                            ErrorMessage="Email Address should be in proper format." ForeColor="Red" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                     
                    </td>
                  </tr>
                  <tr>
                    <td><label>Call Taken by</label>
                        <asp:TextBox ID="txtCall_takenby" runat="server" TabIndex="12"></asp:TextBox>
                        <%--<input name="input7" type="text" />--%>
                    </td>
                  </tr>
                  
                </table>
              </li>
              <li class="last" >
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                  <tr>
                    <td><h3>Google Map</h3></td>
                  </tr>
                  <tr>
                    <td>
                       <div id="map" style="width: 100%; border: 1px; height: 199px; margin-left: 0px;border-style:solid;">
                                <img id="ImgNoGps" runat="server" src="" alt="" width="172" height="168" />
                            </div>
                        <%--<img src="/img/google.png" width="172" height="162" />--%>
                    </td>
                  </tr>
                </table>
              </li>
        </ul>
        <ul>
          <li class="last" style="width:100%" >
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="33%"><label>Follow Up 1</label>
                    <asp:TextBox ID="txtFolowup_1" runat="server" TabIndex="13"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFolowup_1">
                        </asp:CalendarExtender>
                    <%--<input type="text" name="textfield3" id="textfield3" />--%>
                  </td>
                <td width="33%"><label>Follow Up 2</label>
                    <asp:TextBox ID="txtFolowup_2" runat="server" TabIndex="14"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFolowup_2">
                        </asp:CalendarExtender>
                    <%--<input type="text" name="textfield4" id="textfield4" />--%>
                  </td>
                <td width="33%"><label>Follow Up 3</label>
                    <asp:TextBox ID="txtFolowup_3" runat="server" TabIndex="15"></asp:TextBox>                    
                     <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFolowup_3">
                        </asp:CalendarExtender>
                    <%--<input type="text" name="textfield5" id="textfield5" />--%>
                  </td>
              </tr>
            </table>
          </li>
        </ul>
        <div class="btn_sec"></div>
          </div>
	</div>
	<h3><a href="#">Details: Shutters</a></h3>
	<div>
		<div class="form_panel_custom">
<ul>
             <li>
                <table id="tblshutter" runat="server" border="0" cellspacing="0" cellpadding="0">
                  
                  
                  <tr>
                    <td><label>Shutter Top:<span>*</span></label>
                        <asp:TextBox ID="txtshuttertop" runat="server" TabIndex="16"></asp:TextBox>
                        <%-- <input name="input" type="text" />--%>
                   <label></label>     

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please enter Shutter Top." 
                            ControlToValidate="txtshuttertop" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                  </tr>
                  <tr>
                    <td><label>Style<span>*</span></label>
                        <asp:TextBox ID="txtStyle" runat="server" TabIndex="18"></asp:TextBox>
                        <%--<input name="input3" type="text" />--%>
                      <label></label>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please enter Style." 
                            ControlToValidate="txtStyle" ForeColor="Red"></asp:RequiredFieldValidator>
                     
                    </td>
                  </tr>
                  <tr>
                    <td><label>Color</label>
                        <%--<select name="select2">
                        <option>Select</option>
                    </select>--%>
                        <asp:DropDownList ID="ddlColor" runat="server" TabIndex="20">
                            <asp:ListItem>RED</asp:ListItem>
                            <asp:ListItem>BLACK</asp:ListItem>
                            <asp:ListItem>WHITE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td><label>Surface of Mount</label>
                        <asp:DropDownList ID="ddlSurface_mount" TabIndex="22" runat="server">
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select name="select3">
                        <option>Select</option>
                    </select>--%>
                    </td>
                  </tr>
                    <tr>
                    <td><label>Width</label>
                        <asp:DropDownList ID="ddlWidth" runat="server" TabIndex="23">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select name="select3">
                        <option>Select</option>
                    </select>--%>
                    </td>
                  </tr>
                    <tr>
                    <td><label>Height</label>
                        <asp:DropDownList ID="ddlHeight" runat="server" TabIndex="24">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select name="select3">
                        <option>Select</option>
                    </select>--%>
                    </td>
                  </tr>
                    <tr>
                    <td><label>Quantity</label>
                        <asp:DropDownList ID="ddlQnty" runat="server" TabIndex="25">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList>
                        <%-- <select name="select3">
                        <option>Select</option>
                    </select>--%>
                    </td>
                  </tr>
                </table>
          </li>
              <li >
                <table border="0" cellspacing="0" cellpadding="0">
                    <%--   <tr>
                    <td>
                        <asp:Button ID="btn_pdf" runat="server" class="pdf_btn_sec" Text="" />                  
                    </td>
                  </tr>--%>
                  <tr>
                    <td><label>Work Area<span>*</span></label>
                        <asp:TextBox ID="txtWork_area" runat="server" TabIndex="17"></asp:TextBox>
                        <%--<input name="input" type="text" />--%>
                    <label></label>      

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please enter Work area." 
                            ControlToValidate="txtWork_area" ForeColor="Red"></asp:RequiredFieldValidator>
                      </td>
                  </tr>
                  <tr>
                    <td><label>Location Picture<span>*</span></label>
                        <asp:FileUpload ID="flpUploadLoc_img" runat="server" TabIndex="19" />
                    <label></label>     
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please Select Location picture." 
                            ControlToValidate="flpUploadLoc_img" ForeColor="Red"></asp:RequiredFieldValidator>
                     
                    </td>
                  </tr>                   
               
                      <tr>
                    <td><label>Special Instruction<span>*</span></label>
                        <asp:TextBox ID="txtSpcl_inst" runat="server" Rows="5" TextMode="MultiLine" TabIndex="21"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="estimatebutton"
                            ErrorMessage="Please Enter Special Instruction." 
                            ControlToValidate="txtSpcl_inst" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="requiredspecialinstruction" runat="server"  
                           ControlToValidate="txtSpcl_inst" ForeColor="Red" Display="Dynamic" ErrorMessage="Minimum length should be 25 characters" 
                           ValidationExpression=".{25}.*" />
                      </td>
                  </tr>
                  
                </table>
           </li>
              <li class="last" >
                <table border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td>
                        <asp:Image ID="imgplc_Loc" runat="server" Width="60%" Height="88" />

                     <%--<img src="" alt="Image Placeholder" name="imageplaceholder" width="60%" 
                            height="88" id="imgplc_Loc" />--%>

                        <%--<asp:Button ID="btnsold" runat="server" Text="" class="sold_btn_sec"/>                   
                    &nbsp;&nbsp;
                        <asp:Button ID="btnedit" runat="server" Text="" class="edit_btn_sec"/> --%>                
                    </td>
                  </tr>
                  <tr>
                    <td>
                        <%--    <label>Status<span>*</span></label>
                        <asp:TextBox ID="txtStatus" runat="server" Width="80px" TextMode="MultiLine" Rows="5"></asp:TextBox>     --%>            
                    </td>
                  </tr>
                 
                </table>
              </li>
        </ul>
        <div class="btn_sec"></div>
          </div>
	</div>
	<%--<h3><a href="#">Roofing</a></h3>
	<div>
		<p>
		Nam enim risus, molestie et, porta ac, aliquam ac, risus. Quisque lobortis.
		Phasellus pellentesque purus in massa. Aenean in pede. Phasellus ac libero
		ac tellus pellentesque semper. Sed ac felis. Sed commodo, magna quis
		lacinia ornare, quam ante aliquam nisi, eu iaculis leo purus venenatis dui.
		</p>
		<ul>
			<li>List item one</li>
			<li>List item two</li>
			<li>List item three</li>
		</ul>
	</div>
	<h3><a href="#">ETC</a></h3>
	<div>
		<p>
		Cras dictum. Pellentesque habitant morbi tristique senectus et netus
		et malesuada fames ac turpis egestas. Vestibulum ante ipsum primis in
		faucibus orci luctus et ultrices posuere cubilia Curae; Aenean lacinia
		mauris vel est.
		</p>
		<p>
		Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus.
		Class aptent taciti sociosqu ad litora torquent per conubia nostra, per
		inceptos himenaeos.
		</p>
	</div>--%>
    
</div>
<div style="margin-top:20px;"></div>
<div class="btn_sec">

<asp:Button ID="btnedit" runat="server" Text="EDIT EST." onclick="btnedit_Click" ValidationGroup="estimatebutton" TabIndex="26"/>

    <asp:Button ID="btncreateEsti" runat="server" Text="CREATE EST."    ValidationGroup="estimatebutton" TabIndex="27"
        onclick="btncreateEsti_Click" />
    <asp:Button ID="btnAdd" runat="server" Text="ADD" ValidationGroup="add" TabIndex="28"/>
    <%--<input name="" type="submit" value="CREATE EST." /> 
            <input name="" type="submit" value="ADD" />--%>
            </div>
</div>



</div>
</asp:Content>
