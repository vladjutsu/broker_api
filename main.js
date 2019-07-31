var _user = {
    "userId": 0,
    "name": "",
    "secondName": "",
    "email": "",
    "password": "",
    "phone": ""
    }; 

$(document).ready(function () {

    PageLoad();
    $("#login_btn").on("click", Login);

            
});

function setCookie(){
    var date = new Date();
 var minutes = 1;
 date.setTime(date.getTime() + (minutes * 60 * 1000));
 $.cookie("example", "foo", { expires: date });
}

function getCookie(){
    alert(document.cookie);
}


function PageLoad(){
    var mydata = {
                "userId": 1,
                "password": "123"
                };

        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(_user),
            url: "https://uvcbroker.azurewebsites.net/api/user/pageload",
            contentType: "application/json",
            success : function(result, status, xhr)
            {
                _user = result;
                if (_user.userId != 0)
                 response = result;
            }
            });
}


function Login(){
    var email = $("#inp_login_email")[0].value;
    var pass = $("#inp_login_pass")[0].value;

    var userdata = {
        "email": email,
        "password": pass
        };

    var url = "https://localhost:5001?" + "email=" + email + "&password="+pass;

    $.ajax({
            type: 'POST',
            dataType: 'json',
            url: url,
            contentType: "application/json",
            success : function(result, status, xhr)
            {
                _user = result;
                if (_user.userId == 0){
                    alert("no such user");
                    $("#Modal_Login").modal('hide');
                }
                else{
                    $("#menu_btn_login").hide();
                     $("#menu_btn_reg").hide();
                    $("#Modal_Login").modal('hide');
                    $("#user_name")[0].innerText = _user.name;
                    $("#user_name").show();
                }
            }
            });
}


