// colors for normal design
var normalColors = [
    {
        "colorName": "--color1",
        "value": "#FE9900"
    },
    {
        "colorName": "--bgColor",
        "value": "#F5F5F5"
    },
    {
        "colorName": "--fontColor",
        "value": "#282828"
    },
    {
        "colorName": "--blurFontColor",
        "value": "#808080"
    },
    {
        "colorName": "--whiteFontColor",
        "value": "#fff"
    },
    {
        "colorName": "--whiteColor",
        "value": "#fff"
    }
];

// colors for darkmode colors
var darkModeColors = [
    {
        "colorName": "--color1",
        "value": "#FE9900"
    },
    {
        "colorName": "--bgColor",
        "value": "#333"
    },
    {
        "colorName": "--fontColor",
        "value": "#fefefe"
    },
    {
        "colorName": "--blurFontColor",
        "value": "#F5F5F5"
    },
    {
        "colorName": "--whiteFontColor",
        "value": "#fff"
    },
    {
        "colorName": "--whiteColor",
        "value": "#282828"
    }
];



// when open web app check mode in local storage
var modeInLocalStorage = window.localStorage.getItem("mode");
if (modeInLocalStorage === "dark") {

    // set colors on css
    for (var i = 0; i < darkModeColors.length; i++) {
        // set colors on css
        document.documentElement.style.setProperty
            (darkModeColors[i].colorName, darkModeColors[i].value);
    }

    document.getElementById("setDarkMode").style.display = "none";
    document.getElementById("setNormalMode").style.display = "inline";
}
else {
    // store mode on local storage
    window.localStorage.setItem("mode", "normal");

    // set colors on css
    for (var c = 0; c < normalColors.length; c++) {
        document.documentElement.style.setProperty
            (normalColors[c].colorName, normalColors[c].value);
    }

    // hide current btn and show another
    document.getElementById("setNormalMode").style.display = "none";
    document.getElementById("setDarkMode").style.display = "inline";
}

// set dark mode 
document.getElementById("setDarkMode").onclick = function () {
    // remove ancient mode and add new one
    window.localStorage.clear();
    window.localStorage.setItem("mode", "dark");

    // set colors on css
    for (var i = 0; i < darkModeColors.length; i++) {
        document.documentElement.style.setProperty
            (darkModeColors[i].colorName, darkModeColors[i].value);
    }

    // hide current btn and show another
    document.getElementById("setDarkMode").style.display = "none";
    document.getElementById("setNormalMode").style.display = "inline";
};

// set normal mode
document.getElementById("setNormalMode").onclick = function () {
    // remove ancient mode and add new one
    window.localStorage.clear();
    window.localStorage.setItem("mode", "normal");

    for (var i = 0; i < normalColors.length; i++) {
        // set colors on css
        document.documentElement.style.setProperty
            (normalColors[i].colorName, normalColors[i].value);
    }

    //// hide current btn and show another
    document.getElementById("setNormalMode").style.display = "none";
    document.getElementById("setDarkMode").style.display = "inline";
};

