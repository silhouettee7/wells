select
    d.name as department,
    w.name as well_name,
    m.measurement_time::date as measurement_date,
    mt.name as measurement_type,
    min(m.value) as min_value,
    max(m.value) as max_value,
    count(*) as measurement_count
from measurements m
join wells w on m.well_id = w.id
join departments d on w.department_id = d.id
join measurement_types mt on m.measurement_type_id = mt.id
group by d.name, w.name, m.measurement_time::date, mt.name
order by d.name, w.name, measurement_date, mt.name;