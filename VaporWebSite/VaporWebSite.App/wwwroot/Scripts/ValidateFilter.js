function validateFilterForm() {
    let lp = document.forms["Filter"]["lowPrice"].value;
    let hp = document.forms["Filter"]["highPrice"].value;
    let lr = document.forms["Filter"]["lowRating"].value;
    let hr = document.forms["Filter"]["highRating"].value;
    if (lp >= hp) {
        alert("Minimum price must be less than maximum price!")
        lp.focus();
        hp.focus();
        return false;
    }
    if (lr >= hr) {
        alert("Minimum rating must be less than maximum rating!")
        lr.focus();
        hr.focus();
        return false;
    }
    return (true)
}