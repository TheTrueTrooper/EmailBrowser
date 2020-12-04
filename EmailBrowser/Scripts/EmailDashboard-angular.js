/* WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Dec 1,2020
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: to create a angularjs Contoller system to streamline work with APIs
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
*/
angular.module("NGEmailDashboard", ["ngRoute", "ngSanitize", "ngCookies"])
    //.value("$", $)//import 
    //.run(function () {
    //    //not used init set up here
    //})
    //.config(["$routeProvider", function ($routeProvider) {
    //not used yet
    //    $routeProvider
    //        .when("/OverView",
    //            {
    //                templateUrl: "http://localhost:62740/Project/IndexSubPage?AngularView=OverView",
    //                controller: "OverViewController"
    //            });
    //}])
    //.factory("socketService", function ($, $rootScope) {
    //    //Not used
    //})
    .service("WebAPIServices", function ($http) {
        const MailHostURL = "http://localhost:7297/Mail/";
        return {
            /**
             * The Calls for mail box operations
             */
            Mail: {
                /**
                 * Gets all of the mail boxes with an overview of them
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                /**
                 * @returns {object} A object containing all of the data for the mail boxes
                 */
                GetMailBoxes: function () {
                    return $http.get(MailHostURL + "GetMailBoxes", { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("CreateMailBox")]
                 * public ActionResult CreateNewMailBox(string NewBoxName)
                 */
                /**
                 * Creates a new mail box with the name of
                 * @param {string} NewBoxName the name to make the new mail box
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                CreateMailBox: function (NewBoxName) {
                    return $http.post(MailHostURL + "CreateMailBox", { NewBoxName: NewBoxName }, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("RenameMailBox")]
                 * public ActionResult RenameMailBox(string BoxName, string NewBoxName)
                 */
                /**
                 * Changes the name of a mail box
                 * @param {string} BoxName the name of the box to change
                 * @param {string} NewBoxName the name to make the mail box
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                RenameMailBox: function (BoxName, NewBoxName) {
                    return $http.post(MailHostURL + "RenameMailBox", { BoxName: BoxName, NewBoxName: NewBoxName }, { responseType: "json" });
                },
                /* 
                 * [Route("DeleteMailBox")]
                 * public ActionResult DeleteMailBox(string BoxToDeleteName)
                 */
                /**
                 * Deletes a mail box
                 * @param {string} BoxToDeleteName the name of the box to change
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                DeleteMailBox: function (BoxToDeleteName) {
                    return $http.post(MailHostURL + "RenameMailBox", { BoxToDeleteName: BoxToDeleteName }, { responseType: "json" });
                },
                /*
                 * [HttpGet]
                 * [Route("{FromMailBox}/GetAllEmails")]
                 * public ActionResult GetAllEmails(string FromMailBox)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {string} FromMailBox The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                GetAllEmails: function (FromMailBox) {
                    return $http.get(MailHostURL + FromMailBox + "/GetAllEmails", { responseType: "json" });
                },
                /*
                 * [HttpGet]
                 * [Route("GetEmailAndMarkSeen/{EmailUDI}")]
                 * public ActionResult GetEmailAndMarkSeen(uint EmailUDI)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {string} FromMailBox The mail box to get
                 * @param {number} EmailUDI The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                GetEmailAndMarkSeen: function (FromMailBox, EmailUDI) {
                    return $http.get(MailHostURL + FromMailBox + "/GetEmailAndMarkSeen/" + EmailUDI, { responseType: "json" });
                },
                /*
                 * [HttpGet]
                 * [Route("DeleteEmail/{EmailUDI}")]
                 * public ActionResult DeleteEmail(uint EmailUDI)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {string} FromMailBox The mail box to get
                 * @param {number} EmailUDI The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                DeleteEmail: function (FromMailBox, EmailUDI) {
                    return $http.get(MailHostURL + FromMailBox + "/DeleteEmail/" + EmailUDI, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("MoveEmail/{EmailUDI}")]
                 * public ActionResult MoveEmail(uint EmailUDI, string ToMailBox)
                 */
                /**-----------------------------------------------------------------------------------------------------------------
                 * Gets all the mail from a mail box
                 * @param {number} EmailUDI The mail box to get
                 * @param {string} ToMailBox The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                MoveEmail: function (EmailUDI, ToMailBox) {
                    return $http.post(MailHostURL + "MoveEmail/" + EmailUDI, { ToMailBox: ToMailBox }, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("CopyEmail/{EmailUDI}")]
                 * public ActionResult CopyEmail(uint EmailUDI, string ToMailBox)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {number} EmailUDI The mail box to get
                 * @param {string} ToMailBox The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                CopyEmail: function (EmailUDI, ToMailBox) {
                    return $http.post(MailHostURL + "CopyEmail/" + EmailUDI, { ToMailBox: ToMailBox }, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("SaveEmail")]
                 * public ActionResult SaveEmail(EmailMessage NewEmail, string ToMailBox)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {EmailMessage} NewEmail The mail box to get
                 * @param {string} ToMailBox The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                SaveEmail: function (NewEmail, ToMailBox) {
                    return $http.post(MailHostURL + "SaveEmail", { NewEmail: NewEmail, ToMailBox: ToMailBox }, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("SaveEmail")]
                 * public ActionResult SaveEmail(EmailMessage NewEmail, string ToMailBox)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {EmailMessage} NewEmail The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                SaveEmailLight: function (NewEmail) {
                    return $http.post(MailHostURL + "SaveEmail", { NewEmail: NewEmail }, { responseType: "json" });
                },
                /*
                 * [HttpPost]
                 * [Route("SetEmailFlags/{EmailUDI}")]
                 * public ActionResult SetEmailFlags(uint EmailUDI, EmailFlags EmailFlags)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {number} EmailUDI The mail box to get
                 * @param {EmailFlags} EmailFlags The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                SetEmailFlags: function (EmailUDI, EmailFlags) {
                    return $http.post(MailHostURL + "SaveEmail/" + EmailUDI, { NewEmail: NewEmail, EmailFlags: EmailFlags }, { responseType: "json" });
                },
                /*
                 * [HttpGet]
                 * [Route("MarkEmailAsRead/{EmailUDI}")]
                 * public ActionResult MarkEmailAsRead(uint EmailUDI)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {number} EmailUDI The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                MarkEmailAsRead: function (EmailUDI) {
                    return $http.post(MailHostURL + "MarkEmailAsRead/" + EmailUDI, { responseType: "json" });
                },
                /*
                 * [HttpGet]
                 * [Route("MarkEmailAsUnread/{EmailUDI}")]
                 * public ActionResult MarkEmailAsUnread(uint EmailUDI)
                 */
                /**
                 * Gets all the mail from a mail box
                 * @param {number} EmailUDI The mail box to get
                 * @returns {object} A object containing the status of the call with any resulting data
                 */
                MarkEmailAsUnread: function (EmailUDI) {
                    return $http.post(MailHostURL + "MarkEmailAsUnread/" + EmailUDI, { responseType: "json" });
                }
            },
            CreateNewProjectTask(Data) {
                return $http.post("http://localhost:62740/ProjectAPI/ProjectsCreateTask" + ID, Data, { responseType: "json" });
            }
        };
    })
    .controller("EmailAPIController", function ($window, $scope, $sce, WebAPIServices) {

        function TestForError(Result)
        {
            const MailHostLoginURL = "http://localhost:7297/Mail/Login";

            if (Result.Status === "401") {
                $window.location.href = MailHostLoginURL;
            }
            if (Result.Status !== "200") {
                alert(Result.Message);
                return true;
            }
            return false;
        }

        function ResetEmailSelectOnMailBox() {
            $scope.ReplyEmail = { To: "", CC: "", BCC: "", Subject: "", Body: "" };
            $scope.CurrentEmail = null;
        }

        ResetEmailSelectOnMailBox();

        $scope.CurrentEmailName = false;

        $scope.GetFullEmail = function (Email) {
            if (Email !== null)
                return Email.DisplayName + "<" + Email.Address + ">";
            return "";
        };

        WebAPIServices.Mail.GetMailBoxes().then(function (result) {
            TestForError(result.data);
            $scope.MailBoxes = result.data.Data;
        });

        $scope.CurrentMailBoxName = "INBOX";
        WebAPIServices.Mail.GetAllEmails("INBOX").then(function (result) {
            TestForError(result.data);
            $scope.CurrentMailBox = result.data.Data;
        });

        $scope.SelectMailbox = function (Mailbox) {
            WebAPIServices.Mail.GetMailBoxes().then(function (result) {
                TestForError(result.data);
                $scope.MailBoxes = result.data.Data;
            });

            WebAPIServices.Mail.GetAllEmails(Mailbox).then(function (result) {
                TestForError(result.data);
                $scope.CurrentMailBox = result.data.Data;
            });
            $scope.CurrentMailBoxName = Mailbox;
            $scope.NotInbox = Mailbox !== "INBOX";
            ResetEmailSelectOnMailBox();
        };

        $scope.SelectEmail = function (EmailUDI) {
            WebAPIServices.Mail.GetEmailAndMarkSeen($scope.CurrentMailBoxName, EmailUDI).then(function (result) {
                TestForError(result.data);
                $scope.CurrentEmail = result.data.Data;
                var To = "";
                result.data.Data.To.forEach((Address, index) => {
                    if (index !== 0)
                        To = To + " ,";
                    To = To + Address.DisplayName + "<" + Address.Address + ">";
                });
                var CC = "";
                result.data.Data.CC.forEach((Address, index) => {
                    if (index !== 0)
                        CC = CC + " ,";
                    CC = CC + Address.DisplayName + "<" + Address.Address + ">";
                });
                var BCC = "";
                result.data.Data.BCC.forEach((Address, index) => {
                    if (index !== 0)
                        BCC = BCC + " ,";
                    BCC = BCC + Address.DisplayName + "<" + Address.Address + ">";
                });
                if (result.data.Data.From !== null)
                    $scope.ReplyEmail.To = result.data.Data.From.DisplayName + "<" + result.data.Data.From.Address + ">";
                $scope.ReplyEmail.Subject = "RE: " + result.data.Data.Subject;
                $scope.ReplyEmail.Body = "\n\n\n\n\n\n\nFrom:" + $scope.ReplyEmail.To + "To:" + To + "\nCC:" + CC + "\nBCC:" + BCC + "\nSubject: " + result.data.Data.Subject + "\n\n\n" + result.data.Data.Body;
            });
        };

        $scope.DeleteEmail = function (UDI) {
            WebAPIServices.Mail.DeleteEmail($scope.CurrentMailBoxName, UDI).then(function (result) {
                TestForError(result.data);
            });
            $scope.SelectMailbox($scope.CurrentMailBoxName);
        };

        $scope.Refresh = function () {
            WebAPIServices.Mail.GetMailBoxes().then(function (result) {
                TestForError(result.data);
                $scope.MailBoxes = result.data.Data;

                WebAPIServices.Mail.GetMailBoxes().then(function (result) {
                    TestForError(result.data);
                    $scope.MailBoxes = result.data.Data;
                });

                WebAPIServices.Mail.GetAllEmails("INBOX").then(function (result) {
                    TestForError(result.data);
                    $scope.CurrentMailBox = result.data.Data;
                });
                $scope.CurrentMailBoxName = "INBOX";
                $scope.NotInbox = false;
                ResetEmailSelectOnMailBox();
            });
        };

    });
    //.filter('ArrayToBase64String', function () {
    //    return function (buffer) {
    //        //not used
    //    };
    //});