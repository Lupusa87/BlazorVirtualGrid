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
    SetElementScrollLeft: function (el, val) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollLeft=val;
            return tru;
        }
        else {
            return false;
        }
    },
};
