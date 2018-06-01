 function Comma(Num) { //function to add commas to textboxes
                                        debugger
                                        Num += '';
                                        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                        Num = Num.replace('₹', '');

                                        x = Num.split('.');
                                        x1 = x[0];
                                        x2 = x.length > 1 ? '.' + x[1] : '';
                                        var rgx = /(\d+)(\d{3})/;
                                        //var content = '\f156';
                                        while (rgx.test(x1))
                                            x1 = x1.replace(rgx, '$1' + ',' + '$2');
                                        //String.fromCharCode('\f156')
                                        //String.fromCharCode('&#x20B9')
                                        return '₹' + x1 + x2;
                                        //return Number(x1).toLocaleString('en-IN', { style: 'currency', currency: 'INR', maximumFractionDigits: 0 }) + x2;
                                    }