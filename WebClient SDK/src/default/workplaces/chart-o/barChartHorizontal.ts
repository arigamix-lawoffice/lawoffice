import { D3Type } from 'tessa';
import {
  ChartFunc,
  DataType,
  colorFunc,
  Margin,
  LoadAnimationDuration,
  HoverAnimationDuration
} from './common';

export const barChartFuncHorizontal: ChartFunc = (
  d3: D3Type,
  ref: SVGSVGElement,
  data: DataType | null,
  width: number,
  height: number,
  selectedRow: ReadonlyMap<string, any> | null,
  setSelectedRow: (row: ReadonlyMap<string, any>) => void,
  caption: string,
  init: boolean,
  palette: string,
  selectedColor: string,
  onLoaded?: () => void
) => {
  if (!data) {
    return null;
  }

  const chart = () => {
    const svg = d3.select(ref).attr('viewBox', [0, 0, width, height] as any) as d3.Selection<
      SVGSVGElement,
      undefined,
      null,
      undefined
    >;

    // grid lines
    svg.append('g').attr('class', 'grid').call(xAxisGrid);

    svg.append('g').attr('class', 'grid').call(yAxisGrid);

    // head
    svg
      .append('g')
      .append('text')
      .attr('text-anchor', 'middle')
      .attr('class', 'graph-caption')
      .append('tspan')
      .attr('x', width / 2)
      .attr('y', Margin.top * 0.6)
      .text(caption);

    const g = svg
      .append('g')
      .selectAll('rect')
      .data(data)
      .join('rect')
      .attr('stroke', selectedColor || 'gray')
      .attr('stroke-width', (d: any) => (d.row === selectedRow ? 2 : 0));
    if (!init) {
      g.attr('fill', (_d, index) => color(index as any) as any)
        .attr('y', (_d, i) => y(i as any) || null)
        .attr('x', _d => x(0)!)
        .attr('height', y.bandwidth())
        .transition()
        .duration(LoadAnimationDuration)
        .attr('width', d => x(d.value)! - x(0)!);
      onLoaded && setTimeout(onLoaded, LoadAnimationDuration);
    } else {
      g.attr('fill', (_d, index) => color(index as any) as any)
        .attr('y', (_d, i) => y(i as any) || null)
        .attr('x', _d => x(0)!)
        .attr('height', y.bandwidth())
        .attr('width', d => x(d.value)! - x(0)!);
    }
    g.append('title').text((d: any) => `${d.name}: ${d.value.toLocaleString()}`);
    g.on('mouseenter', function (_d) {
      d3.select(this)
        .attr('width', function (d: any) {
          return x(d.value)! - x(0)! + 15;
        })
        .transition()
        .duration(HoverAnimationDuration);
    }).on('mouseleave', function (_d) {
      d3.select(this)
        .attr('width', function (d: any) {
          return x(d.value)! - x(0)!;
        })
        .transition()
        .duration(HoverAnimationDuration);
    });

    g.on('click', function (d: any) {
      setSelectedRow(d.target.__data__.row);
    });

    svg.append('g').call(xAxis);

    return svg.node();
  };

  const color = colorFunc(d3, data, palette);

  const maxValue = d3.max(data, d => d.value) || 0;

  const x = d3
    .scaleLinear()
    .domain([0, maxValue])
    // .nice()
    .range([Margin.left, width - Margin.right - 15]);

  const y = d3
    .scaleBand()
    .domain(d3.range(data.length) as any)
    .range([Margin.top, height - Margin.bottom])
    .padding(0.1);

  const xAxis = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g.attr('transform', `translate(0,${height - Margin.bottom})`).call(d3.axisBottom(x));

  const xAxisGrid = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g.attr('transform', `translate(0,${height - Margin.bottom})`).call(
      d3
        .axisBottom(x)
        .tickFormat(() => '')
        .tickSize(-(height - Margin.top - Margin.bottom))
    );

  const yAxisGrid = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g.attr('transform', `translate(${Margin.left},0)`).call(
      d3
        .axisLeft(y)
        .tickFormat(() => '')
        .tickSize(-(width - Margin.right - Margin.left - 5))
    );

  const svg = chart();

  return svg;
};
