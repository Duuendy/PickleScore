<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="PickleScore.Web.Pages.User.Usuario" MasterPageFile="~/Views/Site.Master"%>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cadastro de Usuário</h2>

    <asp:Button ID="btnNovoUsuario" runat="server" Text="Novo Usuário" OnClientClick="$('#modalUsuario').modal('show'); return false;" CssClass="btn btn-primary" />
    
    <hr />
    
    <asp:GridView ID="gridUsuario" runat="server" DataKeyNames="Id" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelecionado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="DataInsercao" HeaderText="Data de Inserção" />
            <asp:BoundField DataField="DataAlteracao" HeaderText="Data de Alteração" />
        </Columns>
    </asp:GridView>

    <!--Modal-->
    <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Cadastrar Usuário</h5>
                    <button type="button" class="btn-close" data-dismiss="modal">$times;</button>
                </div>

                <div class="modal-body">
                    <asp:TextBox ID="txtNome" placeholder="Nome Completo" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtSenha" placeholder="Senha" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtCpf" placeholder="CPF" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtEmail" placeholder="E-Mail" runat="server" CssClass="form-control" />
                    <asp:TextBox ID="txtNascimento" placeholder="Data de Nasicmento" runat="server" CssClass="form-control" />
                    <asp:DropDownList ID="ddlPerfil" runat="server" class="form-control"/>

                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-secondary" data-miss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
