const addSpaces = (value) => {
    // Insert spaces into textbox for Credit Card #
    let textbox = document.getElementById("TicketCreditCardNumber");
    console.log('Initial: ' + textbox.value);
    let curStr = (textbox.value).replace(/ /gi, "");
    console.log('Middle: ' + curStr);
    let len = curStr.length;
    let sections = ["", "", "", ""];
    let section = 0;
    for (let i = 0; i < len; i++)
    {
        if (/\d$/.test(curStr.charAt(i)))
        {
            if (sections[section].length == 4 && section < 4)
            {
                section++;
            }
            else if (sections[section].length == 4)
            {
                break;
            }
            sections[section] += curStr.charAt(i);
        }
    }
    
    var newStr = sections[0];
    if (sections[1] != "")
        newStr += ' ' + sections[1];
    if (sections[2] != "")
        newStr += ' ' + sections[2];
    if (sections[3] != "")
        newStr += ' ' + sections[3];
    console.log('Final: ' + newStr);
    textbox.value = newStr;
};

//document.getElementById("TicketCreditCardNumber").addEventListener("input", addSpaces);