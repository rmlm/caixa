var rows = [];

$('table:last tbody tr').each(function(){
    var tr = $(this);
    var className = $(tr).attr('class');

    if(className !== undefined && className !== 'headertable' && className !== 'footertable'){
        var data = $($(tr).find('td')[0]).text().trim();
        var descricao = $($(tr).find('td')[1]).find('span:first').text().trim();
        var debito = $($(tr).find('td')[2]).text().trim();
        var credito = $($(tr).find('td')[3]).text().trim();

        rows.push(data + ";" + descricao + ";" + debito + ";" + credito);
    }
});

let csvContent = rows.join("\n");

console.log(csvContent);