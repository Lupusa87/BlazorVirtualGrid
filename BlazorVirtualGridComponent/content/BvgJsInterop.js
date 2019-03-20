var resizeId;

window.addEventListener("resize", onResize, false);


function onResize() {
    clearTimeout(resizeId);
    resizeId = setTimeout(doneResizing, 100);
    
}

function doneResizing() {
    DotNet.invokeMethodAsync('BlazorVirtualGridComponent', 'InvokeOnResize');
}

function updateCells(IDs, b) {
    let j = 0;
    for (var i = 0; i < b.length; i += 2) {
        if (b[i + 1] === "b") {

            if (document.getElementById("chCell" + IDs[j]) !== null) {
                if (b[i].toLowerCase() === "true") {
                    document.getElementById("chCell" + IDs[j]).checked = true;
                }
                else {
                    document.getElementById("chCell" + IDs[j]).checked = false;
                }


                if (document.getElementById("chCell" + IDs[j]).hidden) {
                    document.getElementById("chCell" + IDs[j]).hidden = false;
                    document.getElementById("spCell" + IDs[j]).hidden = true;
                }
            }
        }
        else {
            if (document.getElementById("spCell" + IDs[j]) !== null) {

                if (document.getElementById("spCell" + IDs[j]).lastChild !== null) {
                    document.getElementById("spCell" + IDs[j]).removeChild(document.getElementById("spCell" + IDs[j]).lastChild);
                }

                var c = document.createTextNode(b[i]);

                document.getElementById("spCell" + IDs[j]).appendChild(c);


                if (document.getElementById("spCell" + IDs[j]).hidden) {
                    document.getElementById("spCell" + IDs[j]).hidden = false;
                    document.getElementById("chCell" + IDs[j]).hidden = true;
                }
            }
        }

        j++;
    }


}

function getTime() {
    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
    var s = addZero(d.getSeconds());
    var ms = addZero(d.getMilliseconds(), 2);
    return h + ":" + m + ":" + s + "." + ms;
}

function addZero(i, j = 1) {
    if (i < 10) {
        i = "0" + i;
    }
    if (j === 2) {
        if (i < 100) {
            i = "0" + i;
        }
    }
    return i;
}


function dragstart1(e, id, dotnetHelper) {

    e.dataTransfer.setData('text', id.toString());

    dotnetHelper.invokeMethodAsync('InvokeDragStartFromJS', id);

}


function drop1(e, el, id, dotnetHelper) {

    if (e.preventDefault) { e.preventDefault(); }
    if (e.stopPropagation) { e.stopPropagation(); }


    dotnetHelper.invokeMethodAsync('InvokeDropFromJS', id, e.dataTransfer.getData("text"));

    document.getElementById(el).removeEventListener('dragstart', null);

    return false;
}



window.BvgJsFunctions = {
    Alert: function (msg) {
        alert(msg);
        return true; 
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
            return rect.top;
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
    SetElementScrollTop: function (el, val) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).scrollTop = val;
            return true;
        }
        else {
            return false;
        }
    },
    SetDivsScrollTop: function (val) {
        if (document.getElementById("FrozenDiv1") !== null) {
            document.getElementById("FrozenDiv1").scrollTop = val;
        }
        if (document.getElementById("NonFrozenDiv1") !== null) {
            document.getElementById("NonFrozenDiv1").scrollTop = val;
        }
        return true;
    },
    GetElementScrollLeft: function (el) {
        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollLeft + document.getElementById(el).clientWidth;
        }
        else {
            return 0;
        }
    },
    GetElementScrollTop: function (el) {
        if (document.getElementById(el) !== null) {
            return document.getElementById(el).scrollTop + document.getElementById(el).clientHeight;
        }
        else {
            return 0;
        }
    },
    SetFocus: function (el) {
        if (document.getElementById("divCell" + el) !== null) {
            document.getElementById("divCell" + el).focus();
            return true;
        }
        else {

            return false;
        }
    },
    UpdateRowContentBatch: function (l) {
       
        updateCells(JSON.parse(Blazor.platform.toJavaScriptString(l)));
        return true;
    }, 
    UpdateCellClassBatch: function (l) {
       
        b = JSON.parse(Blazor.platform.toJavaScriptString(l));


        for (var i = 0; i < b.length; i += 2) {

            if (document.getElementById("divCell" + b[i]) !== null) {
                document.getElementById("divCell" + b[i]).setAttribute("class", b[i + 1]);
            }
        }
       
        return true;
    },
    UpdateCellClassBatchMonoByteArray: function (id, l) {
        IDs = JSON.parse(new TextDecoder("utf-8").decode(Blazor.platform.toUint8Array(id)));
        b = JSON.parse(new TextDecoder("utf-8").decode(Blazor.platform.toUint8Array(l)));

        for (var i = 0; i < b.length; i += 1) {

            if (document.getElementById("divCell" + IDs[i]) !== null) {
                document.getElementById("divCell" + IDs[i]).setAttribute("class", b[i]);
            }
        }

        return true;
    },
    UpdateRowWidthsBatch: function (id, l) {

        IDs = JSON.parse(Blazor.platform.toJavaScriptString(id));
        b = JSON.parse(Blazor.platform.toJavaScriptString(l));

        for (var i = 0; i < b.length; i += 1) {

            if (document.getElementById("divCell" + IDs[i]) !== null) {
                document.getElementById("divCell" + IDs[i]).setAttribute("style", "width:" + b[i] + "px");
            }
        }
      
        return true;
    },
    UpdateColContentsBatch: function (l) {
        
        b = JSON.parse(Blazor.platform.toJavaScriptString(l));

        for (var i = 0; i < b.length; i += 4) {

                if (document.getElementById("spCol" + b[i]) !== null) {

                    document.getElementById("spCol" + b[i]).removeChild(document.getElementById("spCol" + b[i]).lastChild);

                    var c = document.createTextNode(b[i + 1]);
                    document.getElementById("spCol" + b[i]).appendChild(c);


                    document.getElementById("spCol" + b[i]).setAttribute("style", "width:" + b[i + 3] + "px");
                    document.getElementById("divCol" + b[i]).setAttribute("style", "width:" + b[i + 2] + "px");
                } 
        }

       
        return true;
    },
    SetAttributeBatch: function (l, attr) {
        
        b = JSON.parse(Blazor.platform.toJavaScriptString(l));

        for (var i = 0; i < b.length; i += 2) {

            if (document.getElementById("divCell" + b[i]) !== null) {
                document.getElementById("divCell" + b[i]).setAttribute(Blazor.platform.toJavaScriptString(attr), b[i + 1]);
            }
        }
        
        return true;
    },
    UpdateRowContentBatchMonoByteArray: function (id, l) {
        updateCells(JSON.parse(new TextDecoder("utf-8").decode(Blazor.platform.toUint8Array(id))),
            JSON.parse(new TextDecoder("utf-8").decode(Blazor.platform.toUint8Array(l))));    
        return true;
    },
    SetValueToCheckBox: function (el, val) {
        
        if (document.getElementById(el) !== null) {
            if (val.toLowerCase() === "true") {
                document.getElementById(el).checked = true;
            }
            else {
                document.getElementById(el).checked = false;
            }
        }
  

        return true;
    },
    UpdateStyle: function (id, val) {
       
        b = Blazor.platform.toJavaScriptString(id);
        if (document.getElementById(b) !== null) {
            document.getElementById(b).innerHTML = Blazor.platform.toJavaScriptString(val);
        }
  
        return true;
    },
    handleDragStart: function (el, id, dotnetHelper) {
        if (document.getElementById(el) !== null) {

            document.getElementById(el).addEventListener('dragstart', function (e) { dragstart1(e, id, dotnetHelper); }, true);
            return true;
        }
        else {
            console.log("can't find element");
        }

        return false;
    },
    handleDrop: function (el, id, dotnetHelper) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).addEventListener('drop', function (e) { drop1(e, el, id, dotnetHelper); }, true);
            return true;
        }
        else {
            console.log("can't find element");
        }

        return false;
    }


    
};
