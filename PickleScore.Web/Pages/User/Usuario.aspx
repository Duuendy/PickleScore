<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="PickleScore.Web.Pages.User.Usuario" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Usuário</h2>
    <div style="display:flex; gap: 8px">
        <button id="btnNovoUsuario" type="button" Class="btn btn-primary">Novo Usuário</button>
        <asp:Button ID="btnInativar" runat="server" Text="Inativar" OnClick="btnInativar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnAtivar" runat="server" Text="Ativar" PostBackUrl="~/Pages/User/UsuarioInativo.aspx" CssClass="btn btn-primary" />
    </div>
    
    <hr />
    
    <asp:GridView ID="gridUsuario" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="gridUsuario_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Sobrenome" HeaderText="Sobrenome" />
            <asp:BoundField DataField="Cpf" HeaderText="CPF" />
            <asp:BoundField DataField="Email" HeaderText="E-Mail" />
            <asp:BoundField DataField="Nascimento" HeaderText="Data Nascimento" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"/>
            <asp:BoundField DataField="PerfilId" HeaderText="Perfil" />
            <asp:BoundField DataField="UsuarioInsercao" HeaderText="Usuario Inserção" />
            <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
            <asp:BoundField DataField="UsuarioAlteracao" HeaderText="Usuario Alteração" />
            <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />           
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-secondary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!--Modal-->
    <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Cadastrar Usuário</h5>
                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:TextBox ID="txtNome" placeholder="Nome" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtSobrenome" placeholder="Sobrenome" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtSenha" placeholder="Senha" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtCpf" placeholder="CPF" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtEmail" placeholder="E-Mail" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtNascimento" placeholder="Data de Nascimento" runat="server" CssClass="form-control" />
                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-control"/>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <!--Alerta-->
    <div id="divMensagem" class="alert alert-success alert-dismissible fade d-none" role="alert">
        <span id="lblMensagemTexto"></span>
        <button type="button" class="btn-close" data-dismiss="alert" aria-label="Fechar"></button>
    </div>

    <script>
        $(document).ready(function () {
            $('#btnNovoUsuario').click(function () {
                $('#modalUsuario').modal('show');
            });
        });
    </script>
    
</asp:Content>
