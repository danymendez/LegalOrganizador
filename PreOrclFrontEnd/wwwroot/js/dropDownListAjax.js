
var onBegin = function () {
    console.log("ONBEGIN");

};

var onComplete = function () {
    console.log("ONCOMPLETE");
};

var onSuccess = function (context) {

    console.log(JSON.stringify(context));
};

var onFailed = function (context) {
    console.log(JSON.stringify(context));

};
