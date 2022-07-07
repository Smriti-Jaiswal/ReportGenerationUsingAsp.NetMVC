

var app = angular.module("myApp", []);

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
} ]);

app.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })

           .success(function (e) {

           })

           .error(function () {
           });
    }
} ]);

app.controller('myCtrl', ['$http', '$scope', 'fileUpload', function ($http, $scope, fileUpload) {
    $scope.loadingIcon = false;
    $scope.validate_passid = false;

    $scope.pnl_validation = false;

    $scope.btntab1 = "btn btn-success bigBtn";
    $scope.btntab2 = "btn btn-warning bigBtn";
    $scope.btntab3 = "btn btn-warning bigBtn";
    $scope.btntab4 = "btn btn-warning bigBtn";
    $scope.btntab5 = "btn btn-warning bigBtn";

    $scope.tabpanel1 = true;
    $scope.tabpanel2 = false;
    $scope.tabpanel3 = false;
    $scope.tabpanel4 = false;
    $scope.tabpanel5 = false;

    $scope.TabPanel_Btn = true;
    $scope.TabPanel1_B = true;
    $scope.TabPanel2_B = false;
    $scope.TabPanel3_B = false;
    $scope.TabPanel4_B = false;
    $scope.TabPanel5_B = false;
    $scope.TabPanel6_B = false;

    $scope.btntab2_en = true;
    $scope.btntab3_en = true;
    $scope.btntab4_en = true;
    $scope.btntab5_en = true;

    $scope.btnclick1 = false;
    $scope.btnclick2 = false;
    $scope.btnclick3 = false;
    $scope.btnclick4 = false;
    $scope.btnclick5 = false;

    $scope.passid = "";

    $scope.pcallcode = "";
    $scope.ccallcode = "";
    $scope.mcallcode = "";

    $scope.PatternCheck = function (txt, pat, prm) {
        var res = validatePatern(txt, pat, prm);
        return res;
    };




    $scope.isValidTab1 = function () {
        var flag = false;
        if ($scope.ddlsalstd != "" && $scope.ddlsalstd != null && $scope.txtfname != "" && $scope.txtfname != null && $scope.txtpersonalcontact != "" && $scope.txtpersonalcontact != null &&
             $scope.DateControl1 != "" && $scope.DateControl1 != null && $scope.PatternCheck($scope.DateControl1, "^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))", "") &&
             $scope.txtmailid != "" && $scope.txtmailid != null && $scope.PatternCheck($scope.txtmailid, "([a-zA-Z0-9]+@((?!lincoln.edu.my).([a-zA-Z0-9]+)\\.+[a-zA-Z0-9]{2,}))", "i") &&
             $scope.ddlcountry != "" && $scope.ddlcountry != null && $scope.ddlcourse != "" && $scope.ddlcourse != null && $scope.ddlintake != "" && $scope.ddlintake != null &&
             $scope.rbpmaritalst != "" && $scope.rbpmaritalst != null && $scope.rbpgender != "" && $scope.rbpgender != null && $scope.ddlhishestquali != "" && $scope.ddlhishestquali != null &&
             $scope.ddlreligion != "" && $scope.ddlreligion != null && $scope.ddlrace != "" && $scope.ddlrace != null && $scope.ddlstate != "" && $scope.ddlstate != null 
             //$scope.rbp_visatype != "" && $scope.rbp_visatype != null && $scope.ddlstdcatagory != "" && $scope.ddlstdcatagory != null &&
            // && $scope.rbpaccomodation != "" && $scope.rbpaccomodation != null
             ) {
            if ($scope.pnl_visadata) {
                //if ($scope.txtvisanumber != "" && $scope.txtvisanumber != null && $scope.myFile != null &&
                //$scope.txtvisaissdt != "" && $scope.txtvisaissdt != null && $scope.PatternCheck($scope.DateControl1, "^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))", "") &&
                //$scope.txtvisaexpdt != "" && $scope.txtvisaexpdt != null && $scope.PatternCheck($scope.DateControl1, "^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))", "")) {

                    flag = true;
               // }
            }
            else {
                flag = true;
            }
        }
        return flag;
    };

    $scope.isValidTab2 = function () {
        var flag = false;
        if ($scope.txtpaddress != "" && $scope.txtpcity != "" && $scope.txtppin != "" && $scope.txtpstate != "" &&
            $scope.txtpaddress != null && $scope.txtpcity != null && $scope.txtppin != null && $scope.txtpstate != null) {
            flag = true;
        }
        return flag;
    };

    $scope.isValidTab3 = function () {
        var flag = false;
        if ($scope.ddlfatherssalid != "" && $scope.txtfathersname != "" && $scope.facallcode != "" && $scope.txtfatherscontactno != "" &&
            $scope.ddlmname != "" && $scope.txtmothersname != "" && $scope.macallcode != "" && $scope.txtmotherscontact != "" &&
            $scope.DDLGSALID != "" && $scope.txtgname != "" && $scope.grcallcode != "" && $scope.txtgcno != "" &&
            $scope.ddlfatherssalid != null && $scope.txtfathersname != null && $scope.facallcode != null && $scope.txtfatherscontactno != null &&
            $scope.ddlmname != null && $scope.txtmothersname != null && $scope.facallcode != null && $scope.txtmotherscontact != null &&
            $scope.DDLGSALID != null && $scope.txtgname != null && $scope.grcallcode != null && $scope.txtgcno != null) {
            flag = true;
        }
        return flag;
    };

    $scope.ShowTab1 = function () {
        $scope.tabpanel1 = true;
        $scope.TabPanel1_B = true;
        $scope.tabpanel2 = false;
        $scope.tabpanel3 = false;
        $scope.tabpanel4 = false;
        $scope.tabpanel5 = false;

        $scope.TabPanel_Btn = true;
        $scope.TabPanel2_B = false;
        $scope.TabPanel3_B = false;
        $scope.TabPanel4_B = false;
        $scope.TabPanel5_B = false;

        $scope.btntab1 = "btn btn-success bigBtn";
        $scope.btntab2 = "btn btn-warning bigBtn";
        $scope.btntab3 = "btn btn-warning bigBtn";
        $scope.btntab4 = "btn btn-warning bigBtn";
        $scope.btntab5 = "btn btn-warning bigBtn";
    };
    $scope.ShowTab2 = function () {
        $scope.btnclick1 = true;
        if ($scope.isValidTab1()) {
            $scope.tabpanel1 = false;
            $scope.tabpanel2 = true;
            $scope.tabpanel3 = false;
            $scope.tabpanel4 = false;
            $scope.tabpanel5 = false;

            $scope.TabPanel_Btn = true;
            $scope.TabPanel1_B = false;
            $scope.TabPanel2_B = true;
            $scope.TabPanel3_B = false;
            $scope.TabPanel4_B = false;
            $scope.TabPanel5_B = false;

            $scope.btntab2_en = false;

            $scope.btntab1 = "btn btn-warning bigBtn";
            $scope.btntab2 = "btn btn-success bigBtn";
            $scope.btntab3 = "btn btn-warning bigBtn";
            $scope.btntab4 = "btn btn-warning bigBtn";
            $scope.btntab5 = "btn btn-warning bigBtn";
        }
    };
    $scope.ShowTab3 = function () {
        $scope.btnclick2 = true;
        if ($scope.isValidTab2()) {
            $scope.tabpanel1 = false;
            $scope.tabpanel2 = false;
            $scope.tabpanel3 = true;
            $scope.tabpanel4 = false;
            $scope.tabpanel5 = false;

            $scope.TabPanel_Btn = true;
            $scope.TabPanel1_B = false;
            $scope.TabPanel2_B = false;
            $scope.TabPanel3_B = true;
            $scope.TabPanel4_B = false;
            $scope.TabPanel5_B = false;

            $scope.btntab3_en = false;

            $scope.btntab1 = "btn btn-warning bigBtn";
            $scope.btntab2 = "btn btn-warning bigBtn";
            $scope.btntab3 = "btn btn-success bigBtn";
            $scope.btntab4 = "btn btn-warning bigBtn";
            $scope.btntab5 = "btn btn-warning bigBtn";
        }
    };
    $scope.ShowTab4 = function () {
        $scope.btnclick3 = true;
        if ($scope.isValidTab3()) {
            if ($scope.ddlcountry == "58CD6215-43ED-4B2F-8EAD-47CA744D6C17") {
                $scope.tabpanel1 = false;
                $scope.tabpanel2 = false;
                $scope.tabpanel3 = false;
                $scope.tabpanel4 = true;
                $scope.tabpanel5 = false;

                $scope.TabPanel_Btn = true;
                $scope.TabPanel1_B = false;
                $scope.TabPanel2_B = false;
                $scope.TabPanel3_B = false;
                $scope.TabPanel4_B = true;
                $scope.TabPanel5_B = false;

                $scope.btntab4_en = false;

                $scope.btntab1 = "btn btn-warning bigBtn";
                $scope.btntab2 = "btn btn-warning bigBtn";
                $scope.btntab3 = "btn btn-warning bigBtn";
                $scope.btntab4 = "btn btn-success bigBtn";
                $scope.btntab5 = "btn btn-warning bigBtn";
            } else {

                if ($scope.tabpanel5) {
                    $scope.ShowTab3();
                }
                else {
                    $scope.ShowTab5();
                }
            }
        }
    };

    $scope.ShowTab5 = function () {
        $scope.tabpanel1 = false;
        $scope.tabpanel2 = false;
        $scope.tabpanel3 = false;
        $scope.tabpanel4 = false;
        $scope.tabpanel5 = false;

        $scope.TabPanel1_B = false;
        $scope.TabPanel2_B = false;
        $scope.TabPanel3_B = false;
        $scope.TabPanel4_B = false;
        $scope.TabPanel5_B = false;
        $scope.TabPanel_Btn = false;

        $scope.btntab5_en = false;

        $scope.btntab1 = "btn btn-warning bigBtn";
        $scope.btntab2 = "btn btn-warning bigBtn";
        $scope.btntab3 = "btn btn-warning bigBtn";
        $scope.btntab4 = "btn btn-warning bigBtn";
        $scope.btntab5 = "btn btn-success bigBtn";
    };
    $scope.CopyAddress = function () {
        if ($scope.chk_copy) {
            $scope.txtcaddress = $scope.txtpaddress;
            $scope.txtccity = $scope.txtpcity;
            $scope.txtcpin = $scope.txtppin;
            $scope.txtcstate = $scope.txtpstate;
            $scope.ddlccountry = $scope.ddlpcountry;
            $scope.txtccn1 = $scope.txtpcn1;
            $scope.txtccn4 = $scope.txtpcn4;
            $scope.ccallcode = $scope.pcallcode;
        }
    };

    $scope.CrsChange = function () {
        $scope.loadingIcon = true;
        $scope.ddlintake = null;
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetIntakeData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { crsid: $scope.ddlcourse }
        }
        $http(httpreq).success(function (response) {

            bindIntake(response.d);
            $scope.loadingIcon = false;
        })
    };
    $scope.IntChange = function () {
        $scope.loadingIcon = true;
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetFeesData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { countryid: $scope.ddlcountry, crsid: $scope.ddlcourse, intid: $scope.ddlintake, passid: $scope.passid }
        }
        $http(httpreq).success(function (response) {

            $scope.txtstdkptfees = response.d.txtstdkptfees;
            $scope.txtlucscholarship = response.d.txtlucscholarship;
            $scope.txtcurrlucfees = response.d.txtcurrlucfees;
            $scope.txttotalsem = response.d.txttotalsem;
            bindFeesData(response.d);

        })
        $scope.loadingIcon = false;
    };

    $scope.CntryChange = function () {
        $scope.IntChange();
        $scope.loadingIcon = true;
        $scope.ddlstate = null;
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetStateData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { countryid: $scope.ddlcountry }
        }
        $http(httpreq).success(function (response) {
            bindState(response.d);
            $scope.loadingIcon = false;
        })

        var httpreq1 = {
            method: 'POST',
            url: 'stdnew.aspx/GetCountryData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { countryid: $scope.ddlcountry }
        }
        $http(httpreq1).success(function (response) {

            if (response.d.islocal == "0") {
                //rbpfinance.SelectedValue = "4F1C9138-DAE0-4A52-BB2C-0D0FBF4CF9E6";
                //$('.rbp_visatype').find('input').each(functionrbp_visatype.SelectedIndex = 1;
                $scope.rbp_visatype = "Student Visa[Airport Pickup]";
              //  $scope.pnl_visadata = true;
            }
            else {
                //rbp_visatype.SelectedIndex = 1;
                // rbpfinance.SelectedIndex = 1;
                $scope.rbp_visatype = "Local";
                $scope.pnl_visadata = false;
            }
            if (!(response.d.countrycallingcode == null || response.d.countrycallingcode == "")) {
                $scope.mcallcode = response.d.countrycallingcode;
                var pattext = "^" + $scope.mcallcode;
                if ($scope.PatternCheck($scope.txtpersonalcontact, pattext, "")) {
                    var reg = new RegExp(pattext);
                    $scope.txtpersonalcontact = $scope.txtpersonalcontact.replace(reg, "");
                }
            }
            $scope.loadingIcon = false;
        })
    };
    $scope.pCountryChange = function () {
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetCountryData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { countryid: $scope.ddlpcountry }
        }
        $http(httpreq).success(function (response) {
            if (!(response.d.countrycallingcode == null || response.d.countrycallingcode == "")) {
                $scope.pcallcode = response.d.countrycallingcode;
                var pattext = "^" + $scope.pcallcode;
                if ($scope.PatternCheck($scope.txtpcn1, pattext, "")) {
                    var reg = new RegExp(pattext);
                    $scope.txtpcn1 = $scope.txtpcn1.replace(reg, "");
                }
                if ($scope.PatternCheck($scope.txtpcn4, pattext, "")) {
                    var reg = new RegExp(pattext);
                    $scope.txtpcn4 = $scope.txtpcn4.replace(reg, "");
                }
            }
            $scope.loadingIcon = false;
        })
    };
    $scope.cCountryChange = function () {
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetCountryData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { countryid: $scope.ddlpcountry }
        }
        $http(httpreq).success(function (response) {
            if (!(response.d.countrycallingcode == null || response.d.countrycallingcode == "")) {
                $scope.ccallcode = response.d.countrycallingcode;
                var pattext = "^" + $scope.ccallcode;
                if ($scope.PatternCheck($scope.txtccn1, pattext, "")) {
                    var reg = new RegExp(pattext);
                    $scope.txtccn1 = $scope.txtccn1.replace(reg, "");
                }
                if ($scope.PatternCheck($scope.txtccn4, pattext, "")) {
                    var reg = new RegExp(pattext);
                    $scope.txtccn4 = $scope.txtccn4.replace(reg, "");
                }
            }
            $scope.loadingIcon = false;
        })
    };
    $scope.fetchFeesData = function () {
        $scope.loadingIcon = true;
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/GetFeesData',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { crsid: $scope.ddlcourse }
        }
        $http(httpreq).success(function (response) {

            bindFeesData($scope, response.d);
            $scope.loadingIcon = false;
        })
    };

    $scope.fetchStudentData = function () {

        if ($scope.passid.length > 0) {
            $scope.loadingIcon = true;
            var httpreq = {
                method: 'POST',
                url: 'stdnew.aspx/GetStudentData',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: { passicid: $scope.passid }
            }
            $http(httpreq).success(function (response) {
                $scope.validate_passid = false;
                if (response.d.length > 0) {
                    var i = response.d[0];

                    $scope.txtfname = i.fname;
                    $scope.txtmname = i.mname;
                    $scope.txtlname = i.lname;

                    $scope.DateControl1 = i.dob;

                    $scope.txtpersonalcontact = i.pmob;
                    $scope.txtmailid = i.email;

                    $scope.ddlcountry = i.pcountry;
                    $scope.CntryChange();

                    $scope.ddlcourse = i.courseid;
                    $scope.CrsChange();

                    $scope.rbpmaritalst = i.maritalsts;
                    $scope.rbpgender = i.sex;
                    $scope.birthdate = i.dob;

                    $scope.txtpaddress = i.add1;
                    $scope.txtpcity = i.citi1;
                    $scope.txtppin = i.pin1;
                    $scope.txtpstate = i.state1;
                    $scope.ddlpcountry = i.country1;
                    $scope.txtpcn1 = i.mob1;
                    $scope.txtpcn4 = i.resno1;
                    $scope.pCountryChange();

                    $scope.txtcaddress = i.add2;
                    $scope.txtccity = i.citi2;
                    $scope.txtcpin = i.pin2;
                    $scope.txtcstate = i.state2;
                    $scope.ddlccountry = i.country2;
                    $scope.txtccn1 = i.mob2;
                    $scope.txtccn4 = i.resno2;
                    $scope.cCountryChange();

                    $scope.txtfathersname = i.faname;
                    $scope.txtfatherscontactno = i.famob;

                    $scope.txtmothersname = i.maname;
                    $scope.txtmotherscontact = i.mamob;
                    $scope.txtgname = i.gname;
                    $scope.txtgcno = i.gmob;
                    $scope.pnl_validation = true;

                   
                }
                else {
                    alert("Data not Found. Please verify  the passport No.");

                }
              
            })
        }
        else {
            $scope.validate_passid = true;
        }
        $scope.pnl_validation = true;
        $scope.loadingIcon = false;
    };
    $scope.uploadFile = function (file) {

        console.log('file is ');
        console.dir(file);

        var uploadUrl = "UploadHandler.ashx";
        fileUpload.uploadFileToUrl(file, uploadUrl);

    };
    $scope.FormDataSubmit = function () {
        $scope.loadingIcon = true;
        var filename = "";

        if ($scope.ddlcountry != "58CD6215-43ED-4B2F-8EAD-47CA744D6C17") {
            //var file = $scope.myFile;
            var file = "LODL";
            filename = file.name;
            $scope.uploadFile(file);
        }
        var d = [{

            'passid': $scope.passid,
            'stdsalid': $scope.ddlsalstd,
            'fname': $scope.txtfname,
            'mname': $scope.txtmname,
            'lname': $scope.txtlname,

            'dob': $scope.DateControl1,

            'pmob': ($scope.mcallcode + $scope.txtpersonalcontact),
            'email': $scope.txtmailid,

            'pcountry': $scope.ddlcountry,
            'courseid': $scope.ddlcourse,
            'intakeid': $scope.ddlintake,
            'maritalsts': $scope.rbpmaritalst,
            'sex': $scope.rbpgender,

            'hishestquali': $scope.ddlhishestquali,
            'religion': $scope.ddlreligion,
            'race': $scope.ddlrace,

            'state': $scope.ddlstate,
            //'visatype': $scope.rbp_visatype,
            //'stdcatagory': $scope.ddlstdcatagory,
            //'visanumber': $scope.txtvisanumber,
            //'visaissdt': $scope.txtvisaissdt,
            //'visaexpdt': $scope.txtvisaexpdt,
          //  'accomodation': $scope.rbpaccomodation,


            'add1': $scope.txtpaddress,
            'citi1': $scope.txtpcity,
            'pin1': $scope.txtppin,
            'state1': $scope.txtpstate,
            'country1': $scope.ddlpcountry,
            'mob1': ($scope.ccallcode + $scope.txtpcn1),
            'resno1': ($scope.ccallcode + $scope.txtpcn4),

            'add2': $scope.txtcaddress,
            'citi2': $scope.txtccity,
            'pin2': $scope.txtcpin,
            'state2': $scope.txtcstate,
            'country2': $scope.ddlccountry,
            'mob2': ($scope.ccallcode + $scope.txtccn1),
            'resno2': ($scope.ccallcode + $scope.txtccn4),

            'fsalid': $scope.ddlfatherssalid,
            'faname': $scope.txtfathersname,
            'famob': ($scope.facallcode + $scope.txtfatherscontactno),
            'msalid': $scope.ddlmname,
            'maname': $scope.txtmothersname,
            'mamob': ($scope.macallcode + $scope.txtmotherscontact),
            'gsalid': $scope.DDLGSALID,
            'gname': $scope.txtgname,
            'gmob': ($scope.grcallcode + $scope.txtgcno),
            'rco': $scope.hdn_rco,
            'filename': filename,

            'SPM_English': $scope.tztcrdtsobtained0,
            'SPM_Behasa_Malaysia': $scope.tztcrdtsobtained2,
            'SPM_Biology': $scope.tztcrdtsobtained4,
            'SPM_Chemistry': $scope.tztcrdtsobtained6,
            'SPM_Physics': $scope.tztcrdtsobtained8,
            'SPM_Mathematics': $scope.tztcrdtsobtained10,
            'SPM_Additional_Mathematics': $scope.tztcrdtsobtained12,
            'SPM_Other_Subjects': $scope.tztcrdtsobtained23,
            'SPM_Other_Subjects_Credit': $scope.tztcrdtsobtained14,
            'STPM_Muet_English': $scope.tztcrdtsobtained16,
            'STPM_Biology': $scope.tztcrdtsobtained18,
            'STPM_Chemistry': $scope.tztcrdtsobtained20,
            'STPM_Physics': $scope.tztcrdtsobtained22,
            'STPM_Mathematics': $scope.tztcrdtsobtained24
        }];
        var data = angular.toJson(d);
        var httpreq = {
            method: 'POST',
            url: 'stdnew.aspx/InsertStudentDetails',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { std: data }
        }
        $http(httpreq).success(function (response) {

            var data = angular.fromJson(response.d);
            if (data.Status == "1") {
                $scope.TabPanel5_B = false;
                document.location.href = "../redirect/redirectafterreg.aspx?id=" + data.Student + "&aprtid=" + "";
            }
            else {
                if (!(data.Message == null || data.Message == "")) {
                    alert(data.Message);
                }
                else {
                    alert('Student Application Submition Unsuccessful ! Please try again later.');
                }
            }
            $scope.loadingIcon = false;
        }).error(function () {
            $scope.loadingIcon = false;
            $scope.TabPanel5_B = false;
            alert("Unable to submit ! Please try again later.");
        })
    };
} ]);

//$(function () {
//    $('.ddlcourse').change(function () {
//        bindIntake();
//    });
//});
function validatePatern(txt, pat, prm) {
    /*var re = pat;
    return re.test(txt);*/
    var regex;
    if (!(prm == null || prm == "")) {
        regex = new RegExp(pat, prm);
    }
    else {
        regex = new RegExp(pat); //  /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    }
    if (regex.test(txt) == true) {       
       return true;
    }
    else {       
       return false;
    }

}

/*$("#ContentPlaceHolder1_txtmailid").keyup(function () {
    alert("Handler for .keyup() called.");
});*/


function bindIntake(response) {
    $(".ddlintake").empty();
    $(".ddlintake").append('<option value="">Select Intake</option>');
    if (response.length > 0) {
        $.each(response, function (i) {
            $(".ddlintake").append('<option value="' + response[i].Value + '">' + response[i].Text + '</option>');
        });
    }
}

function bindState(response) {
    $(".ddlstate").empty();
    $(".ddlstate").append('<option value="">Select State</option>');
    if (response.length > 0) {
        $.each(response, function (i) {
            $(".ddlstate").append('<option value="' + response[i].Value + '">' + response[i].Text + '</option>');
        });
    }
}

function bindFeesData(response) {
    $(".gvstdsppeal").empty();
    var n = 1;
    $(".gvstdsppeal").append('<tr><th>Sl.No.</th><th>Semester</th><th>Due Date</th><th>KPT Fees</th><th>Lincoln Foundation Amount</th><th>Actual Course Fees</th></tr>');
    if (response.FeesGridData.length > 0) {
        $.each(response.FeesGridData, function (i) {
            $(".gvstdsppeal").append('<tr><td>' + n + '</td><td>' + response.FeesGridData[i].semestername + ' </td><td> ' + response.FeesGridData[i].semesterstartdate + '</td><td>' + response.FeesGridData[i].txtgvkptfees + '</td><td>' + response.FeesGridData[i].txtgvscholrshipamnt + '</td><td>' + response.FeesGridData[i].txtgvtotalcrsfees + '</td></tr>');
            n = n + 1;
        });
        $(".gvstdsppeal").append('<tr><td></td><td></td><td><b>Total</b></td><td><b>' + response.txttotalgvkptfees + '</b></td><td><b>' + response.txttotalgvscholrshipamnt + '</b></td><td><b>' + response.txttotalgvtotalcrsfees + '</b></td></tr>');
    }
    else {
        $(".gvstdsppeal").append('<tr><td colspan="100%">No Course Structure Found</td></tr>');
    }
}

