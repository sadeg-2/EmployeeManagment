window.exportToPdf = (title, headers, rows) => {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    doc.setFontSize(16);
    doc.text(title, 10, 15);

    doc.autoTable({
        startY: 25,
        head: [headers],
        body: rows
    });

    doc.save("export.pdf");
};
