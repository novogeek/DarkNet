// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    function getToken() {
        var iframe = $('#frmImplicit');
        $("#ddFriends").change(function () {
            var user = $('option:selected', this).val();
            var idpUrl = $('#hdnImpersonationIDPUrl').val();
            var idpRetUrl = $('#hdnImpersonationRetUrl').val();
            var idpTokenUrl = idpUrl + '?username='+user+'&' + idpRetUrl;
            iframe.attr('src', idpTokenUrl);
        });
        iframe.on('load', () => {
            var newHref = document.getElementById('frmImplicit').contentWindow.location.href;
            let params = (new URL(newHref)).searchParams;
            var token = params.get('token');
            console.log('token:', token);

            //$.ajax({
            //    url: 'https://api.sandbox.slcedu.org/api/rest/v1/students/test1',
            //    type: 'GET',
            //    beforeSend: function (xhr) {
            //        xhr.setRequestHeader('Authorization', 'Bearer '+token);
            //    },
            //    data: {},
            //    success: function () { },
            //    error: function () { },
            //});
        });
    }
    getToken();
});