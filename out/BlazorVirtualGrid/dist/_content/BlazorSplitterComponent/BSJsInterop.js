var sliders_array = [];

function beginSliding(e, el, dotnetHelper) {

    dotnetHelper.invokeMethodAsync('InvokePointerDownFromJS', e.clientX, e.clientY);


    var index = sliders_array.findIndex(x => x.id === el);

    if (index > -1) {
        sliders_array[index].slider.onpointermove = function (a) { slide(a, dotnetHelper); };
        sliders_array[index].slider.setPointerCapture(e.pointerId);
    }
    else {
        console.log("can't find element with id " + el);
    }

}

function stopSliding(e, el) {

    var index = sliders_array.findIndex(x => x.id === el);

    if (index > -1) {
        sliders_array[index].slider.onpointermove = null;
        sliders_array[index].slider.releasePointerCapture(e.pointerId);
    }
    else {
        console.log("can't find element with id " + el);
    }
}


function slide(e, dotnetHelper) {
    e.preventDefault();
    dotnetHelper.invokeMethodAsync('InvokeMoveFromJS', e.clientX, e.clientY);
}

window.BsJsFunctions = {
    alertmsg: function (m) {
        alert(m);
        return true;
    },
    stopDrag: function (el) {
        stopSliding(0, el);
    },
    handleDrag: function (el, dotnetHelper) {
        if (document.getElementById(el) !== null) {

            var b = {
                id: el,
                slider: document.getElementById(el)
            };

            b.slider.addEventListener('pointerdown', function (e) { beginSliding(e, el, dotnetHelper); }, true);
            b.slider.addEventListener('pointerup', function (e) { stopSliding(e, el); }, true);


            sliders_array.push(b);

            return true;
        }
        else {
            console.log("can't found element");
        }

        return false;
    },
    unHandleDrag: function (el) {

        var index = sliders_array.findIndex(x => x.id === el);

        if (index > -1) {
            sliders_array.splice(index, 1);
            return true;
        }
        else {
            console.log("can't find element with id " + el);
            return false;
        }

    }
    //setPCapture: function (el, p) {

    //    if (document.getElementById(el) !== null) {
    //        document.getElementById(el).setPointerCapture(p);
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //},
    //releasePCapture: function (el, p) {

    //    if (document.getElementById(el) !== null) {
    //        document.getElementById(el).releasePointerCapture(p);
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //},
};