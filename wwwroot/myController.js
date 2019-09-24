myApp.controller('myController', function ($scope, $http) {

    var urlSite = "";
    $scope.UserManagement = function (ListOfCoach) {
        if (ListOfCoach) {
            $http.get("api/user/pullCoachList").then(function (res) {
                $scope.coachList = res.data;
                $scope.btnName = "רשימת מאמנים לאישור";
                $scope.btnStatus = false;


            });

        } else {

            coachListStatus();
        }


    };

    $scope.confirmStatus = function () {
        $scope.status = false;
        $http.get(urlSite +"api/user/status?phone=" + localStorage.getItem("log")+"&statusNum="+2).then(function (res) {
            if (res.data) {
                $scope.status = true;

            } else {
                $scope.status = false;
            }
        });

    };

    $scope.listUserOfThisCoach = function () {
        $http.get(urlSite+"api/User/pullUsers?phone=" + localStorage.getItem("log")).then(function (res) {

            $scope.Users = res.data;

           
        });
        
    };
    $scope.listUserAndChack = function () {
        $http.get(urlSite +"api/user/chackUser?phone=" + localStorage.getItem("log")).then(function (response) {
            $scope.chackUser = response.data;

        });

    };

 
    $scope.logOut = function () {
        localStorage.clear();
        window.location.href = "index.html";

    };


    $scope.initIndex = function () {
        if (localStorage.getItem("log")) {

            window.location.href = "PageManagementCoach.html";
        }

    };
    $scope.isLogin = function () {
        $scope.log = false;
        if (localStorage.getItem("log")) {
            $scope.log = true;
        } else {

            window.location.href = "index.html";
        }
    };
    $scope.coachListStatus = function () {
        $http.get(urlSite +"api/user/pullStatus").then(function (res) {
            $scope.coachList = res.data;
            $scope.btnName = "רשימת מאמנים כללית";
            $scope.btnStatus = true;


        });

    };
    $scope.updateStatus = function (phone, statusCode) {
        if (statusCode == 0) {
            $http.get(urlSite +"api/user/deleteCoach?phone=" + phone).then(function (res) {
                alert("המאמן נמחק בהצלחה " + phone);
                window.location.href = "ConfirmUsers.html";
            });

        } else if (statusCode >= 1) {
            $http.get(urlSite +"api/user/updateCoach?phone=" + phone + "&status=" + statusCode).then((res) => {
                alert(res.data + "המאמן אושר ");
                window.location.href = "ConfirmUsers.html";
            });
        } 
    };


   


   
    $scope.measurementDetails = [
        { name: "תאריך", Details: "date" , type: "date"},
        { name: "משקל", Details: "Weight", type:"number" },
        { name: "מים", Details: "water", type: "number" },
        { name: "שומן%", Details: "fat", type: "number"},
        { name: "שריר", Details: "muscle", type: "number" },
        { name: "מבנה גוף", Details: "bodyStructure", type: "number"},
        { name: "BMR", Details: "BMR", type: "number"},
        { name: "גיל הגוף", Details: "BodyAge", type: "number"},
        { name: "שומן בטני", Details: "FatBelly", type: "number"},
        { name: "מסת עצם", Details: "BoneMass", type: "number" },
        { name: "חזה ס'מ", Details: "chest", type: "number" },
        { name: "בטן ס'מ", Details: "stomach", type: "number"},
        { name: "אגן ס'מ", Details: "Legs", type: "number"},
        { name: "כמה שייקים", Details: "shack", type: "number"}
        

    ];


    $scope.login = function () {
        $scope.logOrSin = 'loginDivIndex.html?';
        $scope.log = true;
        $scope.sin = false;
      

    };


    $scope.singup = function () {
        $scope.logOrSin = 'singupDivIndex.html?';
        $scope.log = false;
        $scope.sin = true;

    };
    $scope.serchUser = function () {
        $http.get(urlSite +"api/user/allUsers").then(function (response) {
            $scope.usersList = response.data;

        });

    };

    $scope.loginUser = function () {

        let loginUserNumber = $scope.logNumberUser;
        if (loginUserNumber.length == 0)
            return;
        $http.get(urlSite +"api/User/loginUser?phoneUser=" + loginUserNumber).then(function (response) {

            if (response.data === true) {
                localStorage.setItem("user", loginUserNumber);
                window.location.href = "custemer.html";
            } else {
                $scope.userMassage = "מספר הפלאפון לא תקין";
            }

        });


    };
    $scope.loginCoch = function () {
        let numberLOG = $scope.logNumberCoach;
        let passeordlog = $scope.logPass;

        if (parseInt(numberLOG)) {

            $http.get(urlSite +"api/User/loginCoach?phonecoach=" + numberLOG + "&password=" + passeordlog).then(function (response) {
                if (response.data === true) {
                    localStorage.setItem("log", numberLOG);

                   

                    window.location.href = "PageManagementCoach.html";

                } else {

                    $scope.erorrLoginMassege = "מספר הפלאפון או הסיסמה לא נכונים אנא נסה שנית";
                }

             

            });
        } else {
            $scope.erorrLoginMassege = "המספר פלאפון לא תקין";
        }


    };
    $scope.singupBTN = function () {
        let name = $scope.singupFullName;
        let passeord = $scope.singupPass;
        let number = $scope.singupNum;
        if (parseInt(number)) {

            if (name.length > 2) {
                if (passeord.length > 6) {
                    $http.get(urlSite +"api/User/singupCoach?phone=" + number + "&password=" + passeord + "&name=" + name).then(function (response) {
                        if (response.data == true) {

                            $scope.erorrSingupMassege = "ההרשמה בוצעה בהצלחה נא לחכות לאישור המאמן ";

                        } else {
                            $scope.erorrSingupMassege = "מספר הפלאפון קיים במערכת";

                        }

                    });
                } else {
                    $scope.erorrSingupMassege = "יש למלא סיסמא מעל ל 6 תווים";
                }

            } else {
                $scope.erorrSingupMassege = "יש למלא שם מלא תקין";
            }

        } else {
            $scope.erorrSingupMassege = "יש למלא מספר פלאפון תקין";
        }

    };
    $scope.selectUser = function (man) {
        localStorage.setItem("Muserphone", man.phoneUser);
        localStorage.setItem("Musername", man.name);
        $scope.userSelected = man.name;
        window.location.href = "measurement.html";
    };
    $scope.moveTo = function (url) {
        window.location.href = url;
    };

   $scope.chckEndSendNewMeasurement = function () {
       var arr = {};
       $scope.erorrMsg = "";
        for (var i = 0; i < $scope.measurementDetails.length; i++) {

           
            if (arr[$scope.measurementDetails[i].Details] = document.getElementById($scope.measurementDetails[i].Details).value) {
                arr[$scope.measurementDetails[i].Details] = document.getElementById($scope.measurementDetails[i].Details).value;
                $scope.erorrMsg = "";
            } else {

                $scope.erorrMsg = "isEmpty";
            }


          
       }
      let phoneS = localStorage.getItem("Muserphone");
       $http.defaults.headers.post["Content-Type"] = "application/JSON";
       $http({
           url: urlSite +'api/user/post?phoneUser=' + phoneS,
           method: "POST",
           data: JSON.stringify(arr)
       }).then(function (result) {
           localStorage.removeItem("Muserphone");
           alert(result.data);

       });
    

    };

    $scope.newTraining = function () {
        let phone = $scope.Phone;
        let name = $scope.name;
        let coachPhone = localStorage.getItem("log");
        if (parseInt(phone) && phone != null && name != null) {

            $http.get(urlSite +"api/User/addUser?phone=" + phone + "&name=" + name + "&coachPhone=" + coachPhone).then(function (response) {

                if (response.data === true) {
                    $scope.massegeNewUser = "המשתמש נוצר בהצלחה";
                } else {
                    $scope.massegeNewUser = "מתאמן קיים במערכת";
                }
            });

        } else
            return;

    };
    $scope.measurement = function () {
        $scope.nameMeasurement = localStorage.getItem("Musername");
        localStorage.removeItem("Musername");
    }

    $scope.res = function () {

        if (localStorage.getItem("user")) {
            $http.get(urlSite +"api/user/pullRes?phoneUser=" + localStorage.getItem("user")).then(function (response) {
                $scope.Measurements = response.data;
                localStorage.removeItem("user");
            });
        } else {
            window.location.href = "index.html";

        }
    }

});