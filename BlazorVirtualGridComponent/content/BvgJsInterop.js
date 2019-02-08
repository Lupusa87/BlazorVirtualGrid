window.BvgJsFunctions = {
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
    },
    GetElementActualWidth: function (el) {

        if (document.getElementById(el) !== null) {
            let rect = document.getElementById(el).getBoundingClientRect();
            return rect.width;
        }
        else {
            return 0;
        }
    },
    GetScrollLeftPosition: function (el) {

        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollLeft;
        }
        else {
            return 0;
        }
    },
    GetScrollTopPosition: function (el) {

        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollTop;
        }
        else {
            return 0;
        }
    },
    SetScrollLeftPosition: function (el, val) {

        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollLeft = val;
            return true;
        }
        return false;
    },
    SetScrollTopPosition: function (el, val) {

        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollTop = val;
            return true;
        }

        return false;
    },
};
