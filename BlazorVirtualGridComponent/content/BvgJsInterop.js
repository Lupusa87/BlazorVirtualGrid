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

};
