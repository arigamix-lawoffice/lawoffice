import { D3Type } from 'tessa';
import {
  ChartFunc,
  DataType,
  colorFunc,
  Margin,
  LoadAnimationDuration,
  HoverAnimationDuration
} from './common';

export const barChartFuncVertical: ChartFunc = (
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
      g.style('transform', 'scale(1, -1)')
        .attr('fill', (_d, index) => color(index as any) as any)
        .attr('x', (_d, i) => x(i as any) || null)
        .attr('y', -(height - Margin.bottom))
        .attr('width', x.bandwidth())
        .transition()
        .duration(LoadAnimationDuration)
        .attr('height', d => y(0)! - y(d.value)!);

      g.on('mouseenter', function (_d) {
        d3.select(this)
          .attr('height', function (d: any) {
            return y(0)! - y(d.value)! + 15;
          })
          .transition()
          .duration(HoverAnimationDuration);
      }).on('mouseleave', function (_d) {
        d3.select(this)
          .attr('height', function (d: any) {
            return y(0)! - y(d.value)!;
          })
          .transition()
          .duration(HoverAnimationDuration);
      });
      onLoaded && setTimeout(onLoaded, LoadAnimationDuration);
    } else {
      g.attr('fill', (_d, index) => color(index as any) as any)
        .attr('x', (_d, i) => x(i as any) || null)
        .attr('y', d => y(d.value)! + Margin.bottom)
        .attr('width', x.bandwidth())
        .attr('height', d => y(0)! - y(d.value)!);

      g.on('mouseenter', function (_d) {
        d3.select(this)
          .attr('height', function (d: any) {
            return y(0)! - y(d.value)! + 15;
          })
          .attr('y', (d: any) => y(d.value)! + Margin.bottom - 15)
          .transition()
          .duration(HoverAnimationDuration);
      }).on('mouseleave', function (_d) {
        d3.select(this)
          .attr('height', function (d: any) {
            return y(0)! - y(d.value)!;
          })
          .attr('y', (d: any) => y(d.value)! + Margin.bottom)
          .transition()
          .duration(HoverAnimationDuration);
      });
    }
    g.append('title').text((d: any) => `${d.name}: ${d.value.toLocaleString()}`);

    g.on('click', function (d: any) {
      setSelectedRow(d.target.__data__.row);
    });

    svg.append('g').call(yAxis);

    return svg.node();
  };

  const color = colorFunc(d3, data, palette);

  const x = d3
    .scaleBand()
    .domain(d3.range(data.length) as any)
    .range([Margin.left, width - Margin.right])
    .padding(0.1);

  const y = d3
    .scaleLinear()
    .domain([0, d3.max(data, d => d.value) || 0])
    //  .nice()
    .range([height - Margin.bottom - Margin.top, Margin.top]);

  const yAxis = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g
      .attr('transform', `translate(${Margin.left},${Margin.top})`)
      .call(d3.axisLeft(y))
      .call(g => g.select('.domain').remove())
      .call(g => g.selectAll('.tick line').remove());

  const xAxisGrid = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g.attr('transform', `translate(0,${height - Margin.bottom})`).call(
      d3
        .axisBottom(x)
        .tickFormat(() => '')
        .tickSize(-(height - Margin.top - Margin.bottom))
    );

  const yAxisGrid = (g: d3.Selection<SVGGElement, undefined, null, undefined>) =>
    g.attr('transform', `translate(${Margin.left},${Margin.top})`).call(
      d3
        .axisLeft(y)
        .tickFormat(() => '')
        .tickSize(-(width - Margin.right - Margin.left - 5))
    );

  const svg = chart();

  return svg;
};
