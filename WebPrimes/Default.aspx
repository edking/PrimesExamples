<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebPrimes._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form>
        <h2>Check a single number</h2>
    <div class="inner-wrap">
        <label>Number to check</label> <asp:TextBox runat="server" ID="candidate" /><br/>
        <asp:label runat="server" ID="result"></asp:label>
     <asp:Button runat="server" type="submit" name="Submit" value="Check Now" 
        Text="Check Now" onclick="SubmitButton_Click" ID="submitButton"/>
    </div>
        <h2>Find All Primes Between low and high</h2>
    <div class="inner-wrap">
        <label>low boundary</label> 
        <asp:TextBox runat="server" ID="lowBound" /><br/>
        <label>high boundary</label> 
        <asp:TextBox runat="server" ID="highBound" /><br/>
        
     <asp:Button runat="server" type="submit" value="Submit" 
        Text="Submit" onclick="FindPrimesButton_Click" ID="FindPrimesButton"/>
    </div>
    
    <asp:ListView runat="server" ID="primesList">
    <LayoutTemplate>
    <table runat="server" >
      <tr runat="server" id="itemPlaceholder">
      </tr>
    </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr runat="server">
           <td runat="server">
               <asp:Label runat="server" ID="valueLabel" Text='<%#Eval("value")%>' />
           </td>
        </tr>
    </ItemTemplate>
    </asp:ListView>
    </form>
</asp:Content>
