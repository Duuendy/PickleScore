<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="PickleScore.Web.Pages.Profile.PerfilPages" MasterPageFile="~/Views/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Perfil</h2>

    <asp:Label ID="lblNome" runat="server" Text="Nome do Perfil:"></asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="300px" />
    <hr />
    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnInativar" runat="server" Text="Inativar"  OnClick="btnInativar_Click" CssClass="btn btn-secondary" />
    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>

    <asp:Button ID="btnAtivar" runat="server" Text="Ativar" PostBackUrl="~/Pages/Profile/PerfilInativo.aspx" CssClass="btn btn-secondary" />

    <hr />

    <asp:GridView ID="gridPerfis" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
            <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />
        </Columns>
    </asp:GridView>

    <div id="blocoAlteracao" runat="server" visible="false">
        <asp:Label ID="lblNomeAlteracao" runat="server" Text="Nome do Perfil Alteração:"></asp:Label>
        <asp:TextBox ID="txtNomeAlteracao" runat="server" CssClass="form-control" Width="300px" />
        <asp:Button ID="btnSalvarAlteracao" runat="server" Text="Salvar" OnClick="btnSalvarAlteracao_Click" CssClass="btn btn-primary" />
    </div>

    <!--Alerta-->
<%--    <div id="divMensagem" class="alert alert-warning alert-dismissible d-none mt-2" role="alert">
        <span id="lblMensagemTexto"></span>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Fechar"></button>
    </div>--%>
    <div class="modal fade" id="modalSucesso" tabindex="-1" role="dialog" aria-labelledby="modalSucessoLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content text-center p-4">
          <div class="modal-body">
            <div class="mb-3">
              <i class="bi bi-check-circle-fill text-success" style="font-size: 3rem;"></i>
            </div>
            <h4 class="mb-2">Sucesso!</h4>
            <p id="mensagemModal">Cadastro realizado com sucesso.</p>
            <button type="button" class="btn btn-primary mt-3" data-bs-dismiss="modal">OK</button>
          </div>
        </div>
      </div>
    </div>

    <script>
        //function mostrarMensagem(texto, tipo) {
        //    console.log("mostrarMensagem executada", texto, tipo);
        //    const alertClass = {
        //        sucesso: "alert-success",
        //        erro: "alert-danger",
        //        warning: "alert-warning"
        //    }[tipo];

        //    $('#divMensagem')
        //        .removeClass("alert-success alert-danger alert-warning d-none")
        //        .addClass(alertClass + " show")
        //        .css("display", "block");

        //    $('#lblMensagemTexto').text(texto);
        //}
        function mostrarModalSucesso(texto) {
            console.log("mostrarModalSucesso executada", texto);
            $('#mensagemModal').text(texto);
            $('#modalSucesso').modal('show'); // Bootstrap 4
        }
    </script>
</asp:Content>
