// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    function getToken() {
        var iframe = $('#frmImplicit');
        var idpUrl = $('#hdnImpersonationIDPUrl').val();
        var idpRetUrl = $('#hdnImpersonationRetUrl').val();
        var apiBaseUrl = $('#hdnApiBaseUrl').val();
        var loggedInUser = $('#hdnloggedInUser').val();

        $("#ddFriends").change(function () {
            $("#divPosts").html('Fetching posts...');
            var user = $('option:selected', this).val();
            var idpTokenUrl = idpUrl + '?username=' + user + '&' + idpRetUrl;
            iframe.attr('src', idpTokenUrl);
        });
        iframe.on('load', () => {
            var newHref = document.getElementById('frmImplicit').contentWindow.location.href;
            let params = (new URL(newHref)).searchParams;
            var token = params.get('token');
            var apiUrl = apiBaseUrl + '/api/UserDetailsApi/GetPostsOfTargetUser?targetUser=' + loggedInUser;

            $.ajax({
                url: apiUrl,
                crossDomain: true,
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token);
                },
                data: {},
                success: function (data) {
                    var tmpl = $.templates("#myTmpl"); 
                    var html = tmpl.render(data);      
                    $("#divPosts").html(html);         
                },
                error: function (err) {
                    console.log('error: ', err);
                }
            });
        });
    }
    getToken();
});