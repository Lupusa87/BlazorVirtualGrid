



window.BvgJsFunctions = {
    GetElementActualWidth: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.width;
        }
        else {
            return 0;
        }
    },
    GetElementActualHeight: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.height;
        }
        else {
            return 0;
        }
    },
    GetElementActualTop: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.y;
        }
        else {
            return 0;
        }
    },
    GetWindowHeight: function () {
        return window.innerHeight
            || document.documentElement.clientHeight
            || document.body.clientHeight;
       
    },
    SetElementScrollLeft: function (el, val) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollLeft=val;
            return true;
        }
        else {
            return false;
        }
    },
    GetElementScrollLeft: function (el) {
        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollLeft + document.getElementById(el).clientWidth;
        }
        else {
            return 0;
        }
    },
    SetFocus: function (el) {
        if (document.getElementById(el) !== null) {
            console.log("js set focus to " + el);
            document.getElementById(el).focus();
            return true;
        }
        else {
            console.log("js set focus not found " + el);
            return false;
        }
    },
};
